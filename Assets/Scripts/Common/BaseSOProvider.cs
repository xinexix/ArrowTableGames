using UnityEngine;

public abstract class BaseSOProvider<T> : ScriptableObject, IProvider<T>
{
    public abstract T value { get; }
}
