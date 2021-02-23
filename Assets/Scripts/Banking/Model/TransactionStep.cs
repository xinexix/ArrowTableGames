using System;

public class TransactionStep : ITransactionStep
{
    private DateTime _timestamp;
    private string _actor;
    private string _action;
    private string _outcome;
    private int _adjustment = 0;

    public TransactionStep(string actor, string action, string outcome, int? adjustment)
    {
        _timestamp = DateTime.Now;
        _actor = actor;
        _action = action;
        _outcome = outcome;

        if (adjustment.HasValue)
        {
            _adjustment = adjustment.Value;
        }
    }

    public DateTime timestamp => _timestamp;

    public string actor => _actor;

    public string action => _action;

    public string outcome => _outcome;

    public int adjustment => _adjustment;
}
