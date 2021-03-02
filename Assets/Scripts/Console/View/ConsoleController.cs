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
    private IAbortDialog _abortDialog;
    // private IConsoleBar _consoleBar;
    private IBankingFacade _bankingFacade;

    public BaseSOProvider<ICurrencyFormatter> currencyFormatter;

    public BaseProvider<IDialogScrim> dialogScrimProvider;

    public BaseProvider<IBankingDialog> bankingDialogProvider;

    public BaseProvider<IAbortDialog> abortDialogProvider;

    private void Awake()
    {
        _formatter = currencyFormatter.value;
        _dialogScrim = dialogScrimProvider.value;
        _bankingDialog = bankingDialogProvider.value;
        _abortDialog = abortDialogProvider.value;

        var wallet = GetComponent<IProvider<IWalletController>>().value;
        var betSettings = GetComponent<IProvider<IBetSettingsController>>().value;
        var ledger = GetComponent<IProvider<ITransactionLedger>>().value;

        _bankingFacade = new BankController(wallet, betSettings, ledger);
    }

    private void OnEnable()
    {
        _bankingDialog.currencyFormatter = _formatter;

        _dialogScrim.onInteracted += handleScrimInteracted;

        _bankingDialog.onDialogHidden += handleDialogHidden;
        _abortDialog.onDialogHidden += handleDialogHidden;

        _bankingDialog.onDepositRequested += handleDepositRequest;
        _bankingDialog.onCashoutRequested += handleCashoutRequest;
    }

    private void OnDisable()
    {
        _dialogScrim.onInteracted -= handleScrimInteracted;

        _bankingDialog.onDialogHidden -= handleDialogHidden;
        _abortDialog.onDialogHidden -= handleDialogHidden;

        _bankingDialog.onDepositRequested -= handleDepositRequest;
        _bankingDialog.onCashoutRequested -= handleCashoutRequest;
    }

    private void handleScrimInteracted(object sender, EventArgs e)
    {
        if (_bankingDialog.isShowing)
        {
            _bankingDialog.hide();
        }

        if (_abortDialog.isShowing)
        {
            _abortDialog.hide();
        }
    }

    private void handleDialogHidden(object sender, EventArgs e)
    {
        updateScrimVisibility();
    }

    private void updateScrimVisibility()
    {
        var shouldShow = _bankingDialog.isShowing || _abortDialog.isShowing;

        if (shouldShow)
        {
            _dialogScrim.show();
        }
        else
        {
            _dialogScrim.hide();
        }
    }

    private void showBankingDialog(object sender, EventArgs e)
    {
        _bankingDialog.show();
        _dialogScrim.show();
    }

    private void handleDepositRequest(object sender, AmountEventArgs e)
    {
        _bankingFacade.depositFunds(e.amount);
    }

    private void handleCashoutRequest(object sender, EventArgs e)
    {
        var amount = _bankingFacade.cashOut();

        // Hack just for simplicity
        var formattedAmount = amount.ToString("C");
        Debug.Log($"Ticket printed for {formattedAmount}");
    }

    private void hideBankingDialog(object sender, EventArgs e)
    {
        _bankingDialog.hide();
    }
}
