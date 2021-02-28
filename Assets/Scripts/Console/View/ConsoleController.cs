using UnityEngine;

/// <remarks>
/// It appears I can use interfaces in the RequireComponent attribute, and this will protect
/// against removing components that satisfy those requirements.  But of course Unity
/// can't auto-add components to satisfy these requirements.  There is also no Inspector warning,
/// which I was expecting, and thus I'm opting to reference the behaviors directly.
/// </remarks>
[RequireComponent(typeof(WalletProvider))]
[RequireComponent(typeof(BetSettingsProvider))]
[RequireComponent(typeof(TransactionLedgerProvider))]
public class ConsoleController : MonoBehaviour
{
    public BaseSOProvider<ICurrencyFormatter> currencyFormatter;

    private IBankingFacade _bankingFacade;

    private void Awake()
    {
        var wallet = GetComponent<IProvider<IWalletController>>().value;
        var betSettings = GetComponent<IProvider<IBetSettingsController>>().value;
        var ledger = GetComponent<IProvider<ITransactionLedger>>().value;

        _bankingFacade = new BankController(wallet, betSettings, ledger);
    }
}
