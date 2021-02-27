using UnityEngine;

public abstract class BaseProvider<T> : MonoBehaviour, IProvider<T>
{
    public abstract T value { get; }
}
