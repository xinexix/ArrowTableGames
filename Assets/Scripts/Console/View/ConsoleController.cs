using UnityEngine;

[RequireComponent(typeof(WalletProvider))]
[RequireComponent(typeof(BetSettingsProvider))]
[RequireComponent(typeof(TransactionLedgerProvider))]
public class ConsoleController : MonoBehaviour
{
    private IBankingFacade _bankingFacade;

    private void Start()
    {
        var wallet = GetComponent<IProvider<IWalletController>>().value;
        var betSettings = GetComponent<IProvider<IBetSettingsController>>().value;
        var ledger = GetComponent<IProvider<ITransactionLedger>>().value;

        _bankingFacade = new BankController(wallet, betSettings, ledger);
    }
}
