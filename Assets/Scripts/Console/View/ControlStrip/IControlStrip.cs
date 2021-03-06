using System;

public interface IControlStrip
{
    bool isLobbyButtonActive { get; }
    event EventHandler onAccessLobbyRequested;
    void showLobbyButton(bool visible);

    bool isBankingButtonActive { get; }
    event EventHandler onAccessBankingRequested;
    event EventHandler onCloseBankingRequested;
    void enableBanking(bool enabled);
    void activateBanking(bool active);

    bool isBettingEnabled { get; }
    event EventHandler onBetIncreaseRequested;
    event EventHandler onBetDecreaseRequested;
    void showBetControls(bool visible);
    void enableBetting(bool enabled);

    bool isAutoBetButtonActive { get; }
    event EventHandler onAutoBetStartRequested;
    event EventHandler onAutoBetStopRequested;
    void activateAutoBet(bool active);

    bool isSoundButtonActive { get; }
    event EventHandler onSoundToggleRequested;
    void enableSoundControl(bool enabled);
    void activateSoundControl(bool active);
}
