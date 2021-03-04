using System;
using UnityEngine;
using TMPro;

/// <remarks>
/// I considered making this more generic, like CurrencyValuePopulator.  Then I'd have to have
/// something like a BaseBroadcastSOProvider<TValue, TEventArgs> to provide a C# event.
/// </remarks>
public class BalancePopulator : MonoBehaviour
{
    private ICurrencyFormatter _formatter;
    private IWallet _wallet;

    public BaseSOProvider<ICurrencyFormatter> currencyFormatterProvider;
    public BaseSOProvider<IWallet> walletProvider;
    public TMP_Text balanceText;

    private void Start()
    {
        _formatter = currencyFormatterProvider.value;
        _wallet = walletProvider.value;

        _wallet.onBalanceChanged += handleBalanceChanged;

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
