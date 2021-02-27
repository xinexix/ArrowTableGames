using System;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    private GameFacade _activeGame;

    public GameFacade activeGame => _activeGame;

    public event EventHandler onGameStarted;

    public event EventHandler onGameExited;

    public GameObject activeGameContainer;

    public void startGame(GameFacade game)
    {
        exitGame();

        _activeGame = Instantiate<GameFacade>(game, activeGameContainer.transform);

        onGameStarted?.Invoke(this, EventArgs.Empty);
    }

    public void exitGame()
    {
        if (_activeGame == null) return;

        Destroy(_activeGame.gameObject);
        _activeGame = null;

        onGameExited?.Invoke(this, EventArgs.Empty);
    }

    public void hideActiveGame()
    {
        if (_activeGame == null) return;
    }

    public void returnToActiveGame()
    {
        if (_activeGame == null) return;
    }
}
