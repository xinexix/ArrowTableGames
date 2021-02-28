using UnityEngine;

public class TransactionLedgerProvider : BaseProvider<ITransactionLedger>
{
    private ITransactionLedger _ledger;

    public override ITransactionLedger value => _ledger;

    private void Awake()
    {
        _ledger = new TransactionLedger();
    }
}
