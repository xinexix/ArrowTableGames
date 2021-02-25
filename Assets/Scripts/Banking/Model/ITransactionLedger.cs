public interface ITransactionLedger : ITransactionHistory
{
    void appendTransaction(ITransactionRecord transaction);

    void startTransaction(string gameId, int instanceId);
    void progressTransaction(string actor, string action, string outcome, int? adjustment);
    void finalizeTransaction();

    void abortTransaction();
}
