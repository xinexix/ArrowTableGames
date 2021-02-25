using System.Collections.Generic;

public interface IBetSettingsController : IBetSettings
{
    void setBetSteps(List<int> steps);
    void increaseBet();
    void decreaseBet();
    void setBetIndex(int index);
    void setNearestBet(int value);
}
