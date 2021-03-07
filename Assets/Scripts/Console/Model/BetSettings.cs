using System;
using System.Collections.Generic;

public class BetSettings : IBetSettingsController
{
    private float _denomination;
    private IReadOnlyList<int> _betSteps = new List<int>().AsReadOnly();
    private int _betIndex = 0;

    public BetSettings(float denomination)
    {
        _denomination = denomination;
    }

    public float denomination => _denomination;

    public int activeBet => _betIndex < _betSteps.Count ? _betSteps[_betIndex] : 0;

    public event EventHandler onActiveBetChanged;

    public IReadOnlyList<int> betSteps => _betSteps;

    public event EventHandler onBetStepsChanged;

    public bool isMinBet => _betIndex == 0;

    public bool isMaxBet => _betIndex >= _betSteps.Count - 1;

    public void setBetSteps(List<int> steps)
    {
        if (steps == null)
        {
            steps = new List<int>();
        }

        steps.Sort();
        _betSteps = steps.AsReadOnly();
        _betIndex = 0;

        onBetStepsChanged?.Invoke(this, EventArgs.Empty);
        onActiveBetChanged?.Invoke(this, EventArgs.Empty);
    }

    public void increaseBet()
    {
        setBetIndex(_betIndex + 1);
    }

    public void decreaseBet()
    {
        setBetIndex(_betIndex - 1);
    }

    public void setBetIndex(int index)
    {
        // Clamp within the available range; no lower than 0
        var clampedIndex = Math.Max(Math.Min(index, _betSteps.Count - 1), 0);
        updateBetIndex(clampedIndex);
    }

    public void setNearestBet(int value)
    {
        for (var i = _betSteps.Count; i > 0; i--)
        {
            if (_betSteps[i - 1] <= value)
            {
                updateBetIndex(i - 1);
                return;
            }
        }
        updateBetIndex(0);
    }

    private void updateBetIndex(int newIndex)
    {
        if (newIndex != _betIndex)
        {
            _betIndex = newIndex;

            onActiveBetChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
