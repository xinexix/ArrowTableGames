using System;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    [SerializeField]
    private UniqueIdSO _uniqueId;

    public UniqueIdSO uniqueId => _uniqueId;

    public bool isInProgress => true;
}
