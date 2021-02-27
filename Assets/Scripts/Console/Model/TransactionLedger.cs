using System;
using System.Collections.Generic;

public class TransactionLedger : ITransactionLedger
{
    private List<ITransactionRecord> _history = new List<ITransactionRecord>();
    private TransactionRecord _activeRecord;

    public IReadOnlyList<ITransactionRecord> history => _history.AsReadOnly();

    public event EventHandler onTransactionCommitted;

    public ITransactionRecord lastCommittedTransaction => _history.Count > 0 ? _history[_history.Count - 1] : null;

    public ITransactionRecord inProgressTransaction => _activeRecord;

    public event EventHandler onTransactionProgressed;

    public event EventHandler onTransactionAborted;

    public void appendTransaction(ITransactionRecord transaction)
    {
        _history.Add(transaction);

        onTransactionCommitted?.Invoke(this, EventArgs.Empty);
    }

    /// <remarks>
    /// Note that how we should respond if a transaction is already in progress when this is
    /// called should be implemented via a decorator around this instance.
    /// </remarks>
    public void startTransaction(string gameId, int instanceId)
    {
        _activeRecord = createRecord(gameId, instanceId);
    }

    protected virtual TransactionRecord createRecord(string gameId, int instanceId)
    {
        return new TransactionRecord(gameId, instanceId);
    }

    /// <remarks>
    /// Note that if _activeRecord doesn't exist then nothing happens here.  A decorator around
    /// this instance can be used to achieve a particular behavior is that case.
    /// </remarks>
    public void progressTransaction(string actor, string action, string outcome, int? adjustment)
    {
        if (_activeRecord == null) return;

        var step = createStep(actor, action, outcome, adjustment);

        _activeRecord.addStep(step);

        onTransactionProgressed?.Invoke(this, EventArgs.Empty);
    }

    protected virtual TransactionStep createStep(string actor, string action, string outcome, int? adjustment)
    {
        return new TransactionStep(actor, action, outcome, adjustment);
    }

    public void finalizeTransaction()
    {
        if (_activeRecord == null) return;

        var transaction = _activeRecord;
        _activeRecord = null;

        appendTransaction(_activeRecord);
    }

    public void abortTransaction()
    {
        if (_activeRecord == null) return;

        // TODO onTransactionAborted should pass a custom EventArgs that includes this
        // var transaction = _activeRecord;
        _activeRecord = null;

        onTransactionAborted?.Invoke(this, EventArgs.Empty);
    }
}
