using System;
using UnityEngine;

public abstract class BaseDecimalInput : MonoBehaviour, IDecimalInput
{
    public abstract float value { get; }

    public abstract event EventHandler onValueChanged;

    public abstract void resetValue();
}
