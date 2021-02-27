using System;
using UnityEngine;

public abstract class BaseFundAmount : MonoBehaviour, IFundAmount
{
    public abstract float amount { get; }

    public abstract event EventHandler onAmountChanged;

    public abstract void clearAmount();
}
