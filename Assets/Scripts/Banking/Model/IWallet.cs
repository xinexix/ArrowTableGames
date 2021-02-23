using System;

public interface IWallet
{
    int rawBalance { get; }
    string formattedBalance { get; }
    event EventHandler onBalanceChanged;

    int rawWager { get; }
    string formattedWager { get; }
    event EventHandler onWagerChanged;
}
