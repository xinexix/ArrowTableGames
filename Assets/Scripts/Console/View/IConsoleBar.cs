using System;

public interface IConsoleBar
{
    bool isLobbyButtonOn { get; }
    event EventHandler onAccessLobbyRequested;
    void showLobbyButton(bool show);

    bool isBankingButtonOn { get; }
    event EventHandler onAccessBankingRequested;
    void enableBanking(bool enable);
    void handleBankingClosed();

    event EventHandler onBetIncreaseRequested;
    event EventHandler onBetDecreaseRequested;
    void enableBets(bool enable);

    bool isAutoBetButtonOn { get; }
    event EventHandler onAutoBetStartRequested;
    event EventHandler onAutoBetStopRequested;
    void enableAutoBet(bool enable);
    void handleAutoBetStopped();

    bool isSoundButtonOn { get; }
    event EventHandler onSoundToggleRequested;
    void enableSoundControl(bool enable);
}
