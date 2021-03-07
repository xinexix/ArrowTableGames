using System;

public class TransactionStep : ITransactionStep
{
    private DateTime _timestamp;
    private string _actor;
    private string _action;
    private string _outcome;
    private int _adjustment;

    public TransactionStep(string actor, string action, string outcome, int? adjustment)
    {
        _timestamp = DateTime.Now;
        _actor = actor;
        _action = action;
        _outcome = outcome;

        _adjustment = adjustment.HasValue ? adjustment.Value : 0;
    }

    public DateTime timestamp => _timestamp;

    public string actor => _actor;

    public string action => _action;

    public string outcome => _outcome;

    public int adjustment => _adjustment;
}
