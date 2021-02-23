using System;
using System.Collections.Generic;

public class BankControlledLedger : ITransactionHistory
{
    private const string DEFAULT_STANDALONE_ID = "Banking";

    private List<ITransactionRecord> _history = new List<ITransactionRecord>();
    private TransactionRecord _activeRecord;
    private int _instanceCount = 0;
    private string _standaloneId;

    public BankControlledLedger(string standaloneId = DEFAULT_STANDALONE_ID)
    {
        _standaloneId = standaloneId;
    }

    public IReadOnlyList<ITransactionRecord> history => _history.AsReadOnly();

    public event EventHandler onTransactionCommitted;

    public void startTransaction(string gameId)
    {
        finalizeTransaction();

        _activeRecord = createRecord(gameId, _instanceCount++);

        _history.Add(_activeRecord);
    }

    public void finalizeTransaction()
    {
        if (_activeRecord != null)
        {
            _activeRecord = null;

            onTransactionCommitted?.Invoke(this, EventArgs.Empty);
        }
    }

    protected virtual TransactionRecord createRecord(string gameId, int instanceId)
    {
        return new TransactionRecord(gameId, instanceId);
    }

    public void addStep(string actor, string action, string outcome, int? adjustment)
    {
        var isStandaloneStep = _activeRecord == null;

        if (isStandaloneStep)
        {
            startTransaction(_standaloneId);
        }

        var step = createStep(actor, action, outcome, adjustment);

        _activeRecord.addStep(step);

        if (isStandaloneStep)
        {
            finalizeTransaction();
        }
    }

    protected virtual TransactionStep createStep(string actor, string action, string outcome, int? adjustment)
    {
        return new TransactionStep(actor, action, outcome, adjustment);
    }
}
