using System;

public interface IBet
{
    float denomination { get; }

    int activeBet { get; }
    event EventHandler onActiveBetChanged;
}
