using System;
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
    private ICurrencyFormatter _formatter;
    private IDialogScrim _dialogScrim;
    private IBankingDialog _bankingDialog;
    // private IAbortDialog _abortDialog;
    // private IConsoleBar _consoleBar;
    private IBankingFacade _bankingFacade;

    public BaseSOProvider<ICurrencyFormatter> currencyFormatter;

    public BaseProvider<IDialogScrim> dialogScrimProvider;

    public BaseProvider<IBankingDialog> bankingDialogProvider;

    private void Awake()
    {
        _formatter = currencyFormatter.value;
        _dialogScrim = dialogScrimProvider.value;
        _bankingDialog = bankingDialogProvider.value;

        var wallet = GetComponent<IProvider<IWalletController>>().value;
        var betSettings = GetComponent<IProvider<IBetSettingsController>>().value;
        var ledger = GetComponent<IProvider<ITransactionLedger>>().value;

        _bankingFacade = new BankController(wallet, betSettings, ledger);
    }

    private void OnEnable()
    {
        _bankingDialog.currencyFormatter = _formatter;

        _dialogScrim.onInteracted += handleScrimInteracted;

        _bankingDialog.onDialogHidden += handleBankingHidden;
    }

    private void OnDisable()
    {
        _dialogScrim.onInteracted -= handleScrimInteracted;

        _bankingDialog.onDialogHidden -= handleBankingHidden;
    }

    private void handleScrimInteracted(object sender, EventArgs e)
    {
        if (_bankingDialog.isShowing)
        {
            _bankingDialog.hide();
        }
    }

    // TODO temp
    public void processShowBanking()
    {
        _bankingDialog.show();

        updateScrim();
    }

    private void handleBankingHidden(object sender, EventArgs e)
    {
        updateScrim();
    }

    private void updateScrim()
    {
        var shouldShow = _bankingDialog.isShowing;

        if (shouldShow)
        {
            _dialogScrim.show();
        }
        else
        {
            _dialogScrim.hide();
        }
    }
}
