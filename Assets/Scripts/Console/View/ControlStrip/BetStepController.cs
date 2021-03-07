using System;
using UnityEngine;
using UnityEngine.UI;

public class BetStepController : BaseProvider<IStepInput>, IStepInput
{
    private IBetSettings _betSettings;
    private bool _visible = true;
    private bool _enabled = false;

    // TODO hopefully fix IBetSettingsController to just IBetSettings
    public BaseSOProvider<IBetSettingsController> betSettingsProvider;
    public Button betUpButton;
    public Button betDownButton;

    public event EventHandler onStateChanged;
    public event EventHandler onIncreaseInteraction;
    public event EventHandler onDecreaseInteraction;

    public override IStepInput value => this;

    public bool isVisible
    {
        get => _visible;
        set
        {
            if (_visible == value) return;

            _visible = value;

            updateState();

            onStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool isEnabled
    {
        get => _enabled;
        set
        {
            if (_enabled == value) return;

            _enabled = value;

            updateState();

            onStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Start()
    {
        _betSettings = betSettingsProvider.value;

        _betSettings.onActiveBetChanged += handleBetChanged;

        updateState();
    }

    private void handleBetChanged(object sender, EventArgs e)
    {
        updateState();
    }

    private void updateState()
    {
        betUpButton.interactable = isEnabled && !_betSettings.isMaxBet;
        betUpButton.gameObject.SetActive(isVisible);

        betDownButton.interactable = isEnabled && !_betSettings.isMinBet;
        betDownButton.gameObject.SetActive(isVisible);
    }

    public void acceptBetUp()
    {
        onIncreaseInteraction?.Invoke(this, EventArgs.Empty);
    }

    public void acceptBetDown()
    {
        onDecreaseInteraction?.Invoke(this, EventArgs.Empty);
    }
}
