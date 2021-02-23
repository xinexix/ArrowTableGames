using System.Collections.Generic;

public interface IBanking
{
    float denomination { get; }
    IWallet wallet { get; }
    ITransactionHistory ledger { get; }
    IBetSettings betSettings { get; }

    void setBetSteps(List<int> steps);
    void depositFunds(float amount);
    float cashOut();
    void increaseBet();
    void decreaseBet();
    void submitBet(string gameId);
    void addStep(string actor, string action, string outcome, int? adjustment);
    void finalizeTransaction();
    void abortTransaction();
}
