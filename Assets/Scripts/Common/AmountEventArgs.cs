using System;

public class AmountEventArgs : EventArgs
{
    private float _amount;

    public float amount => _amount;

    public AmountEventArgs(float amount)
    {
        _amount = amount;
    }
}
