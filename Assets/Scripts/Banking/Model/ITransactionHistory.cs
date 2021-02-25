using System;
using System.Collections.Generic;

public interface ITransactionHistory
{
    IReadOnlyList<ITransactionRecord> history { get; }
    ITransactionRecord lastCommittedTransaction { get; }
    ITransactionRecord inProgressTransaction { get; }
    event EventHandler onTransactionCommitted;
}
