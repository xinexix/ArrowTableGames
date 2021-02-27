using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueIdProvider : MonoBehaviour
{
    [SerializeField]
    private UniqueIdSO _uniqueId;

    public UniqueIdSO uniqueId => _uniqueId;
}
