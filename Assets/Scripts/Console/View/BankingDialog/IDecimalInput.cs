using System;

public interface IDecimalInput
{
    float inputValue { get; }
    event EventHandler onValueChanged;

    void resetValue();
}
