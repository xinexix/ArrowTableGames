using System;
using UnityEngine;

public class ControlStripController : BaseProvider<IControlStrip>, IControlStrip
{
    private IToggleInput _lobbyToggle;
    private IToggleInput _bankingToggle;
    private IStepInput _betStepper;
    private IToggleInput _autoBetToggle;
    private IToggleInput _soundToggle;

    public BaseProvider<IToggleInput> lobbyToggleProvider;
    public BaseProvider<IToggleInput> bankingToggleProvider;
    public BaseProvider<IStepInput> betStepProvider;
    public BaseProvider<IToggleInput> autoBetToggleProvider;
    public BaseProvider<IToggleInput> soundToggleProvider;

    public bool isLobbyButtonActive => isToggleActive(_lobbyToggle);

    public bool isBankingButtonActive => isToggleActive(_bankingToggle);

    public bool isBettingEnabled => isStepEnabled(_betStepper);

    public bool isAutoBetButtonActive => isToggleActive(_autoBetToggle);

    public bool isSoundButtonActive => isToggleActive(_soundToggle);

    public event EventHandler onAccessLobbyRequested;
    public event EventHandler onAccessBankingRequested;
    public event EventHandler onCloseBankingRequested;
    public event EventHandler onBetIncreaseRequested;
    public event EventHandler onBetDecreaseRequested;
    public event EventHandler onAutoBetStartRequested;
    public event EventHandler onAutoBetStopRequested;
    public event EventHandler onSoundToggleRequested;

    public override IControlStrip value => this;

    private bool isToggleActive(IToggleInput control)
    {
        return control.isVisible && control.isEnabled && control.isActive;
    }

    private bool isStepEnabled(IStepInput control)
    {
        return control.isVisible && control.isEnabled;
    }

    private void Awake()
    {
        _lobbyToggle = lobbyToggleProvider.value;
        _bankingToggle = bankingToggleProvider.value;
        _betStepper = betStepProvider.value;
        _autoBetToggle = autoBetToggleProvider.value;
        _soundToggle = soundToggleProvider.value;
    }

    private void Start()
    {
        _lobbyToggle.onActiveInteraction += handleActiveLobbyInteraction;
        _bankingToggle.onInactiveInteraction += handleInactiveBankingInteraction;
        _bankingToggle.onActiveInteraction += handleActiveBankingInteraction;
        _betStepper.onIncreaseInteraction += handleBetIncreaseInteraction;
        _betStepper.onDecreaseInteraction += handleBetDecreaseInteraction;
        _autoBetToggle.onInactiveInteraction += handleInactiveAutoBetInteraction;
        _autoBetToggle.onActiveInteraction += handleActiveAutoBetInteraction;
        _soundToggle.onInactiveInteraction += handleSoundToggleInteraction;
        _soundToggle.onActiveInteraction += handleSoundToggleInteraction;
    }

    private void handleActiveLobbyInteraction(object sender, EventArgs e)
    {
        onAccessLobbyRequested?.Invoke(this, EventArgs.Empty);
    }

    private void handleInactiveBankingInteraction(object sender, EventArgs e)
    {
        onAccessBankingRequested?.Invoke(this, EventArgs.Empty);
    }

    private void handleActiveBankingInteraction(object sender, EventArgs e)
    {
        onCloseBankingRequested?.Invoke(this, EventArgs.Empty);
    }

    private void handleBetIncreaseInteraction(object sender, EventArgs e)
    {
        onBetIncreaseRequested?.Invoke(this, EventArgs.Empty);
    }

    private void handleBetDecreaseInteraction(object sender, EventArgs e)
    {
        onBetDecreaseRequested?.Invoke(this, EventArgs.Empty);
    }

    private void handleInactiveAutoBetInteraction(object sender, EventArgs e)
    {
        onAutoBetStartRequested?.Invoke(this, EventArgs.Empty);
    }

    private void handleActiveAutoBetInteraction(object sender, EventArgs e)
    {
        onAutoBetStopRequested?.Invoke(this, EventArgs.Empty);
    }

    private void handleSoundToggleInteraction(object sender, EventArgs e)
    {
        onSoundToggleRequested?.Invoke(this, EventArgs.Empty);
    }

    public void showLobbyButton(bool visible)
    {
        _lobbyToggle.isActive = visible;
    }

    public void enableBanking(bool enabled)
    {
        _bankingToggle.isEnabled = enabled;
    }

    public void handleBankingClosed()
    {
        _bankingToggle.isActive = false;
    }

    public void showBetControls(bool visible)
    {
        _betStepper.isVisible = visible;
        _autoBetToggle.isVisible = visible;
    }

    public void enableBetting(bool enabled)
    {
        _betStepper.isEnabled = enabled;
        enableAutoBet(enabled);
    }

    public void enableAutoBet(bool enabled)
    {
        _autoBetToggle.isEnabled = enabled;
    }

    public void handleAutoBetStopped()
    {
        _autoBetToggle.isActive = false;
    }

    public void enableSoundControl(bool enabled)
    {
        _soundToggle.isVisible = enabled;
    }

    public void setSoundControl(bool active)
    {
        _soundToggle.isActive = active;
    }
}
