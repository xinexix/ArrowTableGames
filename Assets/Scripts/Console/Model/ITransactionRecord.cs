using System.Collections.Generic;

public interface ITransactionRecord
{
    string gameId { get; }
    int instanceId { get; }
    int wagerAmount { get; }
    int winAmount { get; }
    IReadOnlyList<ITransactionStep> steps { get; }
}
