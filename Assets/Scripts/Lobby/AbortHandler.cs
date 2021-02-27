using UnityEngine;

public class AbortHandler : MonoBehaviour
{
    private GameFacade _pendingGame;

    public LobbyController lobbyController;

    public GameObject abortPromptRoot;

    private void Start()
    {
        // Hide the prompt initially
        abortPromptRoot.gameObject.SetActive(false);
    }

    public void requestNewGame(GameFacade game)
    {
        // This likely isn't set but this ensures a known state
        _pendingGame = null;

        var activeGame = lobbyController.activeGame;

        if (activeGame == null)
        {
            lobbyController.startGame(game);
        }
        else if (activeGame.uniqueId == game.uniqueId)
        {
            lobbyController.returnToActiveGame();
        }
        else if (!activeGame.isInProgress)
        {
            lobbyController.startGame(game);
        }
        else
        {
            _pendingGame = game;
            abortPromptRoot.gameObject.SetActive(true);
        }
    }

    public void acceptAbort()
    {
        abortPromptRoot.gameObject.SetActive(false);

        lobbyController.startGame(_pendingGame);

        _pendingGame = null;
    }

    public void cancelAbort()
    {
        abortPromptRoot.gameObject.SetActive(false);
        _pendingGame = null;
    }
}
