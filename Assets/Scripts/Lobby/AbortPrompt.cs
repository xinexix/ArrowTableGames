using UnityEngine;

public class AbortPrompt : MonoBehaviour
{
    private GameController _pendingGame;

    public LobbyController lobbyController;

    private void Start()
    {
        // Hide the owner initially
        gameObject.SetActive(false);
    }

    public void requestNewGame(GameController prefab)
    {
        // This likely isn't set but this ensures a known state
        _pendingGame = null;

        var activeGame = lobbyController.activeGame;

        if (activeGame == null)
        {
            lobbyController.startGame(prefab);
        }
        else if (activeGame.uniqueId == prefab.uniqueId)
        {
            lobbyController.returnToActiveGame();
        }
        else if (!activeGame.isInProgress)
        {
            lobbyController.startGame(prefab);
        }
        else
        {
            _pendingGame = prefab;
            gameObject.SetActive(true);
        }
    }

    public void acceptAbort()
    {
        gameObject.SetActive(false);

        lobbyController.startGame(_pendingGame);

        _pendingGame = null;
    }

    public void cancelAbort()
    {
        gameObject.SetActive(false);
        _pendingGame = null;
    }
}
