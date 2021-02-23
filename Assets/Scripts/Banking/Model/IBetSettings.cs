using System;
using System.Collections.Generic;

public interface IBetSettings
{
    IReadOnlyList<int> betSteps { get; }
    event EventHandler onBetStepsChanged;

    int rawBet { get; }
    string formattedBet { get; }
    event EventHandler onBetChanged;
    bool isMinBet { get; }
    bool isMaxBet { get; }
}
