using System;
using System.Collections.Generic;

public class BankController : IBankingFacade, IDisposable
{
    private IWalletController _wallet;
    private IBetSettingsController _betSettings;
    private ITransactionLedger _ledger;
    private int _instanceCount = 0;

    public BankController(
        IWalletController wallet,
        IBetSettingsController betSettings,
        ITransactionLedger ledger
    ) {
        _wallet = wallet;
        _betSettings = betSettings;
        _ledger = ledger;

        _ledger.onTransactionCommitted += handleTransactionCommitted;
        _ledger.onTransactionProgressed += handleTransactionProgressed;
        _ledger.onTransactionAborted += handleTransactionAborted;
    }

    public void Dispose()
    {
        _ledger.onTransactionCommitted -= handleTransactionCommitted;
        _ledger.onTransactionProgressed -= handleTransactionProgressed;
        _ledger.onTransactionAborted -= handleTransactionAborted;
    }

    public void setBetSteps(List<int> steps)
    {
        var currentBet = _betSettings.activeBet;

        _betSettings.setBetSteps(steps);

        _betSettings.setNearestBet(currentBet);
    }

    public void depositFunds(float amount)
    {
        var depositValue = (int)(amount / _wallet.denomination);
        generateDepositAction(Math.Max(depositValue, 0));
    }

    protected virtual void generateDepositAction(int depositValue)
    {
        var transaction = new TransactionRecord("Banking", _instanceCount++);

        var step = new TransactionStep(
            "Player",
            "Deposited funds",
            "Balance updated",
            depositValue
        );

        transaction.addStep(step);

        _ledger.appendTransaction(transaction);
    }

    public float cashOut()
    {
        // Changes nothing if no transaction is in progress
        abortTransaction();

        // Cache amount because the withdraw action should zero the balance
        var withdrawAmount = _wallet.balance * _wallet.denomination;

        generateWithdrawAction(Math.Max(_wallet.balance, 0));

        return Math.Max(withdrawAmount, 0f);
    }

    protected virtual void generateWithdrawAction(int withdrawValue)
    {
        var transaction = new TransactionRecord("Banking", _instanceCount++);

        var step = new TransactionStep(
            "Player",
            "Withdrew funds",
            "Balance zeroed",
            withdrawValue
        );

        transaction.addStep(step);

        _ledger.appendTransaction(transaction);
    }

    public void increaseBet()
    {
        _betSettings.increaseBet();
    }

    public void decreaseBet()
    {
        _betSettings.decreaseBet();
    }

    public void submitBet(string gameId)
    {
        _ledger.startTransaction(gameId, _instanceCount++);

        generateWagerAction(_betSettings.activeBet);
    }

    protected virtual void generateWagerAction(int wagerValue)
    {
        submitAction(
            "Player",
            "Placed wager",
            "Transaction started",
            -wagerValue
        );
    }

    public void submitAction(string actor, string action, string outcome, int? adjustment)
    {
        _ledger.progressTransaction(actor, action, outcome, adjustment);
    }

    public void finalizeTransaction()
    {
        _ledger.finalizeTransaction();
    }

    public void abortTransaction()
    {
        _ledger.abortTransaction();
    }

    protected virtual void handleTransactionCommitted(object sender, EventArgs e)
    {
        _wallet.resolveWager();

        var lastTransaction = _ledger.lastCommittedTransaction;

        if (lastTransaction == null) return;

        _wallet.adjustCredit(lastTransaction.winAmount);
    }

    protected virtual void handleTransactionProgressed(object sender, EventArgs e)
    {
        var pendingTransaction = _ledger.inProgressTransaction;

        if (pendingTransaction == null) return;

        _wallet.setWager(-pendingTransaction.wagerAmount);
    }

    protected virtual void handleTransactionAborted(object sender, EventArgs e)
    {
        _wallet.resolveWager();
    }
}
