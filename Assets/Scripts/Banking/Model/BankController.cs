using System.Collections.Generic;

public class BankController : IBanking
{
    private float _denomination;
    private ICurrencyFormatter _formatter = NullCurrencyFormatter.instance;

    private BankControlledWallet _wallet;
    private BankControlledLedger _ledger;
    private BankControlledBetSettings _betSettings;

    public BankController(float denomination)
    {
        _denomination = denomination;

        initializeModels();
    }

    protected virtual void initializeModels()
    {
        _wallet = createWallet();
        _ledger = createLedger();
        _betSettings = createBetSettings();
    }

    protected virtual BankControlledWallet createWallet()
    {
        return new BankControlledWallet();
    }

    protected virtual BankControlledLedger createLedger()
    {
        return new BankControlledLedger();
    }

    protected virtual BankControlledBetSettings createBetSettings()
    {
        return new BankControlledBetSettings();
    }

    public float denomination => _denomination;

    public IWallet wallet => _wallet;

    public ITransactionHistory ledger => _ledger;

    public IBetSettings betSettings => _betSettings;

    public ICurrencyFormatter formatter
    {
        get
        {
            return _formatter;
        }

        set
        {
            _formatter = value;

            _wallet.formatter = _formatter;
            _betSettings.formatter = _formatter;
        }
    }

    public void setBetSteps(List<int> steps)
    {
        var currentBet = _betSettings.rawBet;

        _betSettings.setBetSteps(steps);

        _betSettings.setNearestBet(currentBet);
    }

    public void depositFunds(float amount)
    {
        var depositAmount = (int)(amount / _denomination);
        generateDepositStep(depositAmount);
        // TODO If a deposit requires no transaction to be active then here we should abort, if necessary
        // however to do that from a deposit request the abortion has to have a way of communicating out
        // to the active game.  At
    }

    protected virtual void generateDepositStep(int depositAmount)
    {
        addStep(
            "Player",
            "Deposited funds",
            "Balance updated",
            depositAmount
        );
    }

    public float cashOut()
    {
        // TODO abort, addStep using -rawBalance
        // I think the ledger needs a isTransactionPending flag
        return 0f;
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
        _ledger.startTransaction(gameId);

        generateWagerStep();
    }

    protected virtual void generateWagerStep()
    {
        addStep(
            "Player",
            "Placed wager",
            "Transaction started",
            -_betSettings.rawBet
        );
    }

    public void addStep(string actor, string action, string outcome, int? adjustment)
    {
        _ledger.addStep(actor, action, outcome, adjustment);

        if (adjustment.HasValue)
        {
            processAdjustment(adjustment.Value);
        }
    }

    protected virtual void processAdjustment(int adjustment)
    {
        if (adjustment < 0)
        {
            _wallet.addWager(-adjustment);
        }
    }

    public void finalizeTransaction()
    {
        _ledger.finalizeTransaction();

        _wallet.finalizeWager();

        processCompletedTransaction(_ledger.lastCommittedTransaction);
    }

    protected virtual void processCompletedTransaction(ITransactionRecord lastTransaction)
    {
        if (lastTransaction == null)
        {
            return;
        }

        _wallet.adjustBalance(lastTransaction.winAmount);
    }

    public void abortTransaction()
    {
        // TODO addStep -current payout, finalize step and wager
    }
}
