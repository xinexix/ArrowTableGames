using System;
using System.Collections.Generic;

public interface ITransactionHistory
{
    IReadOnlyList<ITransactionRecord> history { get; }
    event EventHandler onTransactionCommitted;
}
