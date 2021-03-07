using System;

public class TransactionEventArgs : EventArgs
{
    private ITransactionRecord _transaction;

    public ITransactionRecord transaction => _transaction;

    public TransactionEventArgs(ITransactionRecord transaction)
    {
        _transaction = transaction;
    }
}
