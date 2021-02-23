using System;
using System.Collections.Generic;

public class BankControlledBetSettings : IBetSettings
{
    private IReadOnlyList<int> _betSteps = new List<int>();
    private int _betIndex = 0;
    private ICurrencyFormatter _formatter = NullCurrencyFormatter.instance;

    public ICurrencyFormatter formatter
    {
        get
        {
            return _formatter;
        }
        set
        {
            _formatter = value;

            onBetChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public IReadOnlyList<int> betSteps => _betSteps;

    public event EventHandler onBetStepsChanged;

    public int rawBet => _betIndex < _betSteps.Count ? _betSteps[_betIndex] : 0;

    public string formattedBet => formatter.format(rawBet);

    public event EventHandler onBetChanged;

    public bool isMinBet => _betIndex == 0;

    public bool isMaxBet => _betIndex == _betSteps.Count - 1;

    public void setBetSteps(List<int> steps)
    {
        steps.Sort();
        _betSteps = steps.AsReadOnly();
        _betIndex = 0;

        onBetStepsChanged?.Invoke(this, EventArgs.Empty);
        onBetChanged?.Invoke(this, EventArgs.Empty);
    }

    public void increaseBet()
    {
        updateBetIndex(Math.Min(_betIndex + 1, _betSteps.Count));
    }

    public void decreaseBet()
    {
        updateBetIndex(Math.Max(_betIndex - 1, 0));
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

            onBetChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
