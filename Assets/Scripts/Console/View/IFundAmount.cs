using System;

public interface IFundAmount
{
    float amount { get; }
    event EventHandler onAmountChanged;

    void clearAmount();
}
