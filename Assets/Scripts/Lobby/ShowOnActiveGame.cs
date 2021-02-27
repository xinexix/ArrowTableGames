using System;
using UnityEngine;

public class ShowOnActiveGame : MonoBehaviour
{
    public LobbyController lobbyController;

    public UniqueIdSO comparisonId;

    // Start is called before the first frame update
    private void Start()
    {
        if (lobbyController == null)
        {
            gameObject.SetActive(false);
            return;
        }

        lobbyController.onGameStarted += handleGameStarted;
        lobbyController.onGameExited += handleGameExisted;
    }

    private void handleGameStarted(object sender, EventArgs e)
    {
        var activeGameId = lobbyController.activeGame?.uniqueId;

        gameObject.SetActive(activeGameId == comparisonId);
    }

    private void handleGameExisted(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }
}
