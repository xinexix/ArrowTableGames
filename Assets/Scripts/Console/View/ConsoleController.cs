using System;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleController : BaseProvider<IConsoleFacade>, IConsoleFacade
{
    private IBankingFacade _bankingFacade;
    private IControlStrip _controlStrip;
    private IDialogScrim _dialogScrim;
    private IBankingDialog _bankingDialog;
    private IAbortDialog _abortDialog;
    private string _latestGameId;

    public event EventHandler onShowLobbyRequested;
    public event EventHandler onFocusStolen;
    public event EventHandler onFocusReturned;
    public event EventHandler onTransactionAborted;
    public event EventHandler<BoolEventArgs> onSoundChanged;

    public BaseSOProvider<IWalletController> walletProvider;
    public BaseSOProvider<IBetSettingsController> betSettingsProvider;
    public BaseSOProvider<ITransactionLedger> ledgerProvider;

    public BaseProvider<IControlStrip> controlStripProvider;
    public BaseProvider<IDialogScrim> dialogScrimProvider;
    public BaseProvider<IAbortDialog> abortDialogProvider;
    public BaseProvider<IBankingDialog> bankingDialogProvider;

    public override IConsoleFacade value => this;

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
        _controlStrip.showLobbyButton(false);
        _controlStrip.enableBanking(true);
        _controlStrip.activateBanking(false);
        _controlStrip.showBetControls(false);
        _controlStrip.enableBetting(false);
        _controlStrip.enableSoundControl(true);
        _controlStrip.activateSoundControl(true);

        _controlStrip.onAccessLobbyRequested += handleAccessLobbyRequested;
        _controlStrip.onAccessBankingRequested += handleAccessBankingRequested;
        _controlStrip.onCloseBankingRequested += handleCloseBankingRequested;

        _dialogScrim.onInteracted += handleScrimInteracted;

        _bankingDialog.onDialogHidden += handleBankingDialogHidden;
        _bankingDialog.onDepositRequested += handleDepositRequest;
        _bankingDialog.onCashoutRequested += handleCashoutRequest;

        _abortDialog.onAbortConfirmed += handleAbortConfirmed;
        _abortDialog.onAbortCancelled += handleAbortCancelled;
        _abortDialog.onDialogHidden += handleAbortDialogHidden;

        _controlStrip.onBetIncreaseRequested += handleBetIncreaseRequested;
        _controlStrip.onBetDecreaseRequested += handleBetDecreaseRequested;
        _controlStrip.onAutoBetStartRequested += handleAutoBetStartRequested;
        _controlStrip.onAutoBetStopRequested += handleAutoBetStopRequested;

        _controlStrip.onSoundToggleRequested += handleSoundToggleRequested;
    }

    private void handleAccessLobbyRequested(object sender, EventArgs e)
    {
        onShowLobbyRequested?.Invoke(this, EventArgs.Empty);
    }

    private void handleAccessBankingRequested(object sender, EventArgs e)
    {
        _controlStrip.activateBanking(true);
        _controlStrip.enableBetting(false);

        _bankingDialog.show();
        _dialogScrim.show();

        onFocusStolen?.Invoke(this, EventArgs.Empty);
    }

    private void handleCloseBankingRequested(object sender, EventArgs e)
    {
        _bankingDialog.hide();
    }

    private void handleScrimInteracted(object sender, EventArgs e)
    {
        if (_abortDialog.isShowing)
        {
            _abortDialog.hide();
        }
        if (_bankingDialog.isShowing)
        {
            _bankingDialog.hide();
        }
    }

    private void handleBankingDialogHidden(object sender, EventArgs e)
    {
        _controlStrip.activateBanking(false);
        _controlStrip.enableBetting(true);

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

            onFocusReturned?.Invoke(this, EventArgs.Empty);
        }
    }

    private void handleDepositRequest(object sender, AmountEventArgs e)
    {
        _bankingFacade.depositFunds(e.amount);
    }

    private void handleCashoutRequest(object sender, EventArgs e)
    {
        if (_bankingFacade.isTransactionOpen)
        {
            _controlStrip.enableBanking(false);
            _controlStrip.enableBetting(false);

            _abortDialog.show();
        }
        else
        {
            fulfillCashoutRequest();
        }
    }

    private void fulfillCashoutRequest()
    {
        var amount = _bankingFacade.cashOut();

        // Hack just for simplicity
        var formattedAmount = amount.ToString("C");
        Debug.Log($"Ticket printed for {formattedAmount}");
    }

    private void handleAbortConfirmed(object sender, EventArgs e)
    {
        fulfillCashoutRequest();

        _abortDialog.hide();

        onTransactionAborted?.Invoke(this, EventArgs.Empty);
    }

    private void handleAbortCancelled(object sender, EventArgs e)
    {
        _abortDialog.hide();
    }

    private void handleAbortDialogHidden(object sender, EventArgs e)
    {
        _controlStrip.enableBanking(true);

        updateScrimVisibility();
    }

    public void handleLobbyShown()
    {
        // TODO handle if either dialog is showing

        _controlStrip.showLobbyButton(false);
    }

    public void handleGameEntered(string gameId)
    {
        _latestGameId = gameId;

        _controlStrip.showLobbyButton(true);
    }

    public void setBetSteps(List<int> betSteps)
    {
        _bankingFacade.setBetSteps(betSteps);
    }

    private void handleBetIncreaseRequested(object sender, EventArgs e)
    {
        _bankingFacade.increaseBet();
    }

    private void handleBetDecreaseRequested(object sender, EventArgs e)
    {
        _bankingFacade.decreaseBet();
    }

    private void handleAutoBetStartRequested(object sender, EventArgs e)
    {
        _controlStrip.activateAutoBet(true);
    }

    private void handleAutoBetStopRequested(object sender, EventArgs e)
    {
        _controlStrip.activateAutoBet(false);
    }

    public void submitBet()
    {
        _bankingFacade.submitBet(_latestGameId);
    }

    public void submitAction(string actor, string action, string outcome, int? adjustment)
    {
        _bankingFacade.submitAction(actor, action, outcome, adjustment);
    }

    public void completeTransaction()
    {
        _bankingFacade.finalizeTransaction();
    }

    private void handleSoundToggleRequested(object sender, EventArgs e)
    {
        _controlStrip.activateSoundControl(!_controlStrip.isSoundButtonActive);

        onSoundChanged?.Invoke(this, new BoolEventArgs(_controlStrip.isSoundButtonActive));
    }
}
