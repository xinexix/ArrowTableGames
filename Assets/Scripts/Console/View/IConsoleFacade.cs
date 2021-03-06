using System;

public interface IConsoleFacade
{
    event EventHandler onShowLobbyRequested;
    void handleLobbyShown();
    void handleGameEntered(string gameId);

    event EventHandler onFocusStolen;
    event EventHandler onFocusReturned;
    event EventHandler onTransactionAborted;
}
