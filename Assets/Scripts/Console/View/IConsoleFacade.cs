using System;
using System.Collections.Generic;

public interface IConsoleFacade
{
    event EventHandler onShowLobbyRequested;
    void handleLobbyShown();

    event EventHandler onFocusStolen;
    event EventHandler onFocusReturned;
    event EventHandler onTransactionAborted;
    event EventHandler<BoolEventArgs> onSoundChanged;

    void handleGameEntered(string gameId);
    void setBetSteps(List<int> betSteps);
    void submitBet();
    void submitAction(string actor, string action, string outcome, int? adjustment);
    void completeTransaction();
}
