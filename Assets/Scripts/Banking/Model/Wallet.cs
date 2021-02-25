using System;

public class Wallet : IWalletController
{
    private float _denomination;
    private int _credit = 0;
    private int _wager = 0;

    public Wallet(float denomination)
    {
        _denomination = denomination;
    }

    public float denomination => _denomination;

    public int credit => _credit;

    public event EventHandler onCreditChanged;

    public int balance => _credit - _wager;

    public event EventHandler onBalanceChanged;

    public int wager => _wager;

    public event EventHandler onWagerChanged;

    public void adjustCredit(int offset)
    {
        setCredit(_credit + offset);
    }

    public void setCredit(int value)
    {
        updateCredit(Math.Max(value, 0));
    }

    private void updateCredit(int newValue)
    {
        if (_credit != newValue)
        {
            _credit = newValue;

            onCreditChanged?.Invoke(this, EventArgs.Empty);
            onBalanceChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void adjustWager(int offset)
    {
        setWager(_wager + offset);
    }

    public void setWager(int value)
    {
        updateWager(Math.Max(value, 0));
    }

    private void updateWager(int newValue)
    {
        if (_wager != newValue)
        {
            _wager = newValue;

            onWagerChanged?.Invoke(this, EventArgs.Empty);
            onBalanceChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void resolveWager()
    {
        if (_wager == 0) return;

        _credit -= _wager;
        _wager = 0;

        onWagerChanged?.Invoke(this, EventArgs.Empty);
        onBalanceChanged?.Invoke(this, EventArgs.Empty);
        onCreditChanged?.Invoke(this, EventArgs.Empty);
    }
}
