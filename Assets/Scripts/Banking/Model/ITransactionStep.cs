using System;

public interface ITransactionStep
{
    DateTime timestamp { get; }
    string actor { get; }
    string action { get; }
    string outcome { get; }
    int adjustment { get; }
}
