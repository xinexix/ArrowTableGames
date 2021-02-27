using System;

public interface IDecimalInput
{
    float value { get; }
    event EventHandler onValueChanged;

    void resetValue();
}
