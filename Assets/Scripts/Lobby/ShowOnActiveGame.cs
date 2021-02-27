using System;
using UnityEngine;

public class ShowOnActiveGame : MonoBehaviour
{
    public LobbyController lobbyController;

    public UniqueIdSO comparisonId;

    // Start is called before the first frame update
    private void Start()
    {
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
        var activeGameId = lobbyController.activeGame?.uniqueId;

        gameObject.SetActive(activeGameId == comparisonId);
    }

    private void handleGameExited(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }
}
