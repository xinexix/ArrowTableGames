using System;
using UnityEngine;
using TMPro;

public class BalanceController : BaseProvider<IBalance>, IBalance
{
    private ICurrencyFormatter _formatter;
    private IWallet _wallet;

    public TMP_Text balanceText;

    public override IBalance value => this;

    public ICurrencyFormatter currencyFormatter
    {
        get => _formatter;
        set
        {
            _formatter = value;

            updateBalance();
        }
    }

    public IWallet wallet
    {
        get => _wallet;
        set
        {
            _wallet.onBalanceChanged -= handleBalanceChanged;

            _wallet = value;

            _wallet.onBalanceChanged += handleBalanceChanged;

            updateBalance();
        }
    }

    private void OnEnable()
    {
        updateBalance();
    }

    private void handleBalanceChanged(object sender, EventArgs e)
    {
        updateBalance();
    }

    private void updateBalance()
    {
        balanceText.text = _formatter.format(_wallet.balance);
    }
}
