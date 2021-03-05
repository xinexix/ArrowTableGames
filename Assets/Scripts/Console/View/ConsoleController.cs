using System;
using UnityEngine;

public class ConsoleController : MonoBehaviour, IConsoleFacade
{
    private IBankingFacade _bankingFacade;
    private IControlStrip _controlStrip;
    private IDialogScrim _dialogScrim;
    private IBankingDialog _bankingDialog;
    private IAbortDialog _abortDialog;

    public event EventHandler onShowLobbyRequested;

    public BaseSOProvider<IWalletController> walletProvider;
    public BaseSOProvider<IBetSettingsController> betSettingsProvider;
    public BaseSOProvider<ITransactionLedger> ledgerProvider;

    public BaseProvider<IControlStrip> controlStripProvider;
    public BaseProvider<IDialogScrim> dialogScrimProvider;
    public BaseProvider<IAbortDialog> abortDialogProvider;
    public BaseProvider<IBankingDialog> bankingDialogProvider;

    private void Awake()
    {
        var wallet = walletProvider.value;
        var betSettings = betSettingsProvider.value;
        var ledger = ledgerProvider.value;

        _bankingFacade = new BankController(wallet, betSettings, ledger);

        _controlStrip = controlStripProvider.value;
        _dialogScrim = dialogScrimProvider.value;
        _abortDialog = abortDialogProvider.value;
        _bankingDialog = bankingDialogProvider.value;
    }

    private void Start()
    {
        _controlStrip.onAccessLobbyRequested += handleAccessLobbyRequested;
    }

    private void handleAccessLobbyRequested(object sender, EventArgs e)
    {
        onShowLobbyRequested?.Invoke(this, EventArgs.Empty);
    }

    public void handleLobbyShown()
    {
        _controlStrip.showLobbyButton(false);
    }

    public void handleGameEntered(string gameId)
    {
        _controlStrip.showLobbyButton(true);
    }

    private void OnEnable()
    {
        /*
        _dialogScrim.onInteracted += handleScrimInteracted;

        _bankingDialog.onDialogHidden += handleDialogHidden;
        _abortDialog.onDialogHidden += handleDialogHidden;

        _bankingDialog.onDepositRequested += handleDepositRequest;
        _bankingDialog.onCashoutRequested += handleCashoutRequest;
        */
    }

    private void OnDisable()
    {
        /*
        _dialogScrim.onInteracted -= handleScrimInteracted;

        _bankingDialog.onDialogHidden -= handleDialogHidden;
        _abortDialog.onDialogHidden -= handleDialogHidden;

        _bankingDialog.onDepositRequested -= handleDepositRequest;
        _bankingDialog.onCashoutRequested -= handleCashoutRequest;
        */
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
