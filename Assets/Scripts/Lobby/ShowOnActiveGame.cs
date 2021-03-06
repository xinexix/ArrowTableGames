using System;
using UnityEngine;

public class ShowOnActiveGame : MonoBehaviour
{
    public LobbyController lobbyController;

    public GameIdSO comparisonId;

    private void Start()
    {
        // Hide the owner initially
        gameObject.SetActive(false);

        if (lobbyController == null)
        {
            return;
        }

        lobbyController.onGameStarted += handleGameStarted;
        lobbyController.onGameExited += handleGameExited;
    }

    private void handleGameStarted(object sender, EventArgs e)
    {
        var activeGameId = lobbyController.activeGame?.gameId;

        gameObject.SetActive(activeGameId == comparisonId.value);
    }

    private void handleGameExited(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }
}
