using UnityEngine;

[CreateAssetMenu(menuName = "Script Objects/Transaction Ledger Model")]
public class TransactionLedgerSO : BaseSOProvider<ITransactionLedger>
{
    private ITransactionLedger _ledger;

    public override ITransactionLedger value
    {
        get
        {
            if (_ledger == null)
            {
                _ledger = new TransactionLedger();
            }

            return _ledger;
        }
    }
}
