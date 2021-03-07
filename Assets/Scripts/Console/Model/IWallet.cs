using System;

public interface IWallet
{
    float denomination { get; }

    int credit { get; }
    event EventHandler onCreditChanged;

    int balance { get; }
    event EventHandler onBalanceChanged;

    int wager { get; }
    event EventHandler onWagerChanged;
}
