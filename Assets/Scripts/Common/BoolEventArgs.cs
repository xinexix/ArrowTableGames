using System;

public class BoolEventArgs : EventArgs
{
    private bool _value;

    public bool value => _value;

    public BoolEventArgs(bool value)
    {
        _value = value;
    }
}
