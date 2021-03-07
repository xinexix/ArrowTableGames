using System;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    [SerializeField]
    private BaseSOProvider<string> _gameId;

    public string gameId => _gameId.value;

    public bool isInProgress => true;
}
