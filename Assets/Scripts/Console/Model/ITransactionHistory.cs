using System;
using System.Collections.Generic;

public interface ITransactionHistory
{
    IReadOnlyList<ITransactionRecord> history { get; }
    event EventHandler<TransactionEventArgs> onTransactionCommitted;
    ITransactionRecord lastCommittedTransaction { get; }

    ITransactionRecord inProgressTransaction { get; }
    event EventHandler<TransactionEventArgs> onTransactionProgressed;

    event EventHandler<TransactionEventArgs> onTransactionAborted;
}
