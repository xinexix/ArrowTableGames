using System;

public class AmountEventArgs : EventArgs
{
    private int _amount;

    public int amount => _amount;

    public AmountEventArgs(int amount)
    {
        _amount = amount;
    }
}
