using System;
using UnityEngine;
using TMPro;

/// <remarks>
/// I considered making this more generic, like CurrencyValuePopulator.  But then I'd have
/// to have something like a BaseBroadcastSOProvider<TValue, TEventArgs> to provide a C# event.
/// Doesn't seem worth it at this point.
/// </remarks>
[RequireComponent(typeof(TMP_Text))]
public class SetBalanceText : MonoBehaviour
{
    private ICurrencyFormatter _formatter;
    private IWallet _wallet;
    private TMP_Text _balanceText;

    public BaseSOProvider<ICurrencyFormatter> currencyFormatterProvider;
    // TODO crap, covariance doesn't seem to be working; is that a serialization issue?
    public BaseSOProvider<IWalletController> walletProvider;

    private void Start()
    {
        _formatter = currencyFormatterProvider.value;
        _wallet = walletProvider.value;

        _balanceText = GetComponent<TMP_Text>();

        _wallet.onBalanceChanged += handleBalanceChanged;

        updateText();
    }

    private void handleBalanceChanged(object sender, EventArgs e)
    {
        updateText();
    }

    private void updateText()
    {
        _balanceText.text = _formatter.format(_wallet.balance);
    }
}
