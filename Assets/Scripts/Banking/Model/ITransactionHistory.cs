using System;
using System.Collections.Generic;

public interface ITransactionHistory
{
    IReadOnlyList<ITransactionRecord> history { get; }
    event EventHandler onTransactionCommitted;
    ITransactionRecord lastCommittedTransaction { get; }

    ITransactionRecord inProgressTransaction { get; }
    event EventHandler onTransactionProgressed;

    event EventHandler onTransactionAborted;
}
