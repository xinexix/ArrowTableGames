using System;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    private GameController _activeGame;

    public GameController activeGame => _activeGame;

    public event EventHandler onGameStarted;

    public event EventHandler onGameExited;

    public GameObject activeGameContainer;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void startGame(GameController prefab)
    {
        exitGame();

        _activeGame = Instantiate<GameController>(prefab, activeGameContainer.transform);

        onGameStarted?.Invoke(this, EventArgs.Empty);
    }

    public void exitGame()
    {
        if (_activeGame == null) return;

        Destroy(_activeGame);
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
