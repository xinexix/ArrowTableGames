using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private UniqueIdSO _uniqueId;

    public UniqueIdSO uniqueId => _uniqueId;

    public bool isInProgress => true;

    private void Start()
    {

    }

    private void Update()
    {

    }
}
