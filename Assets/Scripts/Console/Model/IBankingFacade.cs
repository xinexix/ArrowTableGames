using System.Collections.Generic;

public interface IBankingFacade
{
    void setBetSteps(List<int> steps);

    void depositFunds(float amount);
    float cashOut();

    void increaseBet();
    void decreaseBet();

    bool isTransactionOpen { get; }
    void submitBet(string gameId);
    void submitAction(string actor, string action, string outcome, int? adjustment);
    void finalizeTransaction();
    void abortTransaction();
}
