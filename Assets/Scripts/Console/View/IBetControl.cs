using System;

public interface IBetControl
{
    ICurrencyFormatter currencyFormatter { get; set; }
    IWallet wallet { get; set; }
    IBetSettings betSettings { get; set; }

    bool isVisible { get; set; }
    bool isEnabled { get; set; }
    int currentValue { get; }
    bool canBetLower { get; }
    bool canBetHigher { get; }
    event EventHandler onStateChanged;
    event EventHandler onValueChanged;
}
