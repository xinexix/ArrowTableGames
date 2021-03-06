using System;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    private IConsoleFacade _console;
    private GameFacade _activeGame;
    private GameFacade _pendingGame;

    public BaseProvider<IConsoleFacade> consoleProvider;
    public GameObject activeGameContainer;

    // TODO temporary
    public BaseSOProvider<List<int>> betStepsProvider;

    public GameFacade activeGame => _activeGame;

    public event EventHandler onGameStarted;
    public event EventHandler onGameExited;

    private void Awake()
    {
        _console = consoleProvider.value;
    }

    private void Start()
    {
        _console.onShowLobbyRequested += handleShowLobbyRequested;
    }

    private void handleShowLobbyRequested(object sender, EventArgs e)
    {
        hideActiveGame();
    }

    public void hideActiveGame()
    {
        if (_activeGame == null) return;

        activeGameContainer.SetActive(false);
    }

    public void requestNewGame(GameFacade game)
    {
        // This likely isn't set but this ensures a known state
        // TODO actually right now a cancelled abort will leave this set
        _pendingGame = null;

        if (activeGame == null)
        {
            startGame(game);
        }
        else if (activeGame.gameId == game.gameId)
        {
            returnToActiveGame();
        }
        else if (!_console.isTransactionOpen && !activeGame.isInProgress)
        {
            startGame(game);
        }
        else
        {
            _pendingGame = game;
            _console.onTransactionAborted += handleTransactionAborted;
            _console.showAbortDialog();
        }
    }

    private void startGame(GameFacade game)
    {
        exitGame();

        _activeGame = Instantiate<GameFacade>(game, activeGameContainer.transform);

        // TODO temporary
        _console.setBetSteps(betStepsProvider?.value);

        onGameStarted?.Invoke(this, EventArgs.Empty);
    }

    private void exitGame()
    {
        if (_activeGame == null) return;

        _console.setBetSteps(null);

        Destroy(_activeGame.gameObject);
        _activeGame = null;

        onGameExited?.Invoke(this, EventArgs.Empty);
    }

    public void returnToActiveGame()
    {
        if (_activeGame == null) return;

        activeGameContainer.SetActive(true);
    }

    private void handleTransactionAborted(object sender, EventArgs e)
    {
        _console.onTransactionAborted -= handleTransactionAborted;

        if (_pendingGame == null) return;

        startGame(_pendingGame);

        _pendingGame = null;
    }
}
