using System;
using System.Collections.Generic;

public interface IBetSettings : IBet
{
    IReadOnlyList<int> betSteps { get; }
    event EventHandler onBetStepsChanged;

    bool isMinBet { get; }
    bool isMaxBet { get; }
}
