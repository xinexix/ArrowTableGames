using System;

public class BankControlledWallet : IWallet
{
    private int _balance = 0;
    private int _wager = 0;
    private ICurrencyFormatter _formatter = NullCurrencyFormatter.instance;

    public ICurrencyFormatter formatter
    {
        get
        {
            return _formatter;
        }
        set
        {
            _formatter = value;

            onBalanceChanged?.Invoke(this, EventArgs.Empty);
            onWagerChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public int rawBalance => _balance;

    public string formattedBalance => formatter.format(_balance);

    public event EventHandler onBalanceChanged;

    public int rawWager => _wager;

    public string formattedWager => formatter.format(_wager);

    public event EventHandler onWagerChanged;

    public void adjustBalance(int amount)
    {
        _balance += amount;

        onBalanceChanged?.Invoke(this, EventArgs.Empty);
    }

    public void addWager(int amount)
    {
        _wager += amount;
        _balance -= amount;

        onBalanceChanged?.Invoke(this, EventArgs.Empty);
        onWagerChanged?.Invoke(this, EventArgs.Empty);
    }

    public void finalizeWager()
    {
        _wager = 0;

        onWagerChanged?.Invoke(this, EventArgs.Empty);
    }
}
