using System.Collections.Generic;

public class TransactionRecord : ITransactionRecord
{
    private string _gameId;
    private int _instanceId;
    private List<ITransactionStep> _steps = new List<ITransactionStep>();

    public TransactionRecord(string gameId, int instanceId)
    {
        _gameId = gameId;
        _instanceId = instanceId;
    }

    public string gameId => _gameId;

    public int instanceId => _instanceId;

    public int wagerAmount
    {
        get
        {
            var sum = 0;
            foreach (var step in _steps)
            {
                if (step.adjustment < 0)
                {
                    // Subtracting to convert to a positive value
                    sum -= step.adjustment;
                }
            }
            return sum;
        }
    }

    public int winAmount
    {
        get
        {
            var sum = 0;
            foreach (var step in _steps)
            {
                if (step.adjustment > 0)
                {
                    sum += step.adjustment;
                }
            }
            return sum;
        }
    }

    public IReadOnlyList<ITransactionStep> steps => _steps.AsReadOnly();

    public void addStep(ITransactionStep step)
    {
        _steps.Add(step);
    }
}
