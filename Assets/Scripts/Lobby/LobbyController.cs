using System;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    private GameController _activeGame;

    public GameController activeGame => _activeGame;

    public event EventHandler onGameStarted;

    public event EventHandler onGameExited;

    public GameObject activeGameContainer;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void startGame(GameController prefab)
    {
        exitGame();

        // TODO I think I want a base game behavior and instantiate specifically to that
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
