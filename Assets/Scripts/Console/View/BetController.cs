using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BetController : BaseProvider<IBetControl>, IBetControl
{
    private bool _isVisible = true;
    private bool _isEnabled = false;

    public Button betUpButton;
    public Button betDownButton;
    public TMP_Text betText;

    public ICurrencyFormatter currencyFormatter { get; set; }
    public IWallet wallet { get; set; }
    public IBetSettingsController betSettings { get; set; }

    public event EventHandler onStateChanged;
    public event EventHandler onValueChanged;

    public override IBetControl value => this;

    public bool isVisible
    {
        get => _visible;
        set
        {
            _visible = value;
        }
    }

    public bool isEnabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
        }
    }

    public int currentValue => betSettings.activeBet;

    public bool canBetLower => !betSettings.isMinBet;

    public bool canBetHigher => !betSettings.isMaxBet;

    private void Start()
    {
        betSettings.onActiveBetChanged += handleBetChanged;
    }

    public void acceptBetUp()
    {
        betSettings.increaseBet();
    }

    public void acceptBetDown()
    {
        betSettings.decreaseBet();
    }

    private void handleBetChanged(object sender, EventArgs e)
    {
        updateBetText();
    }

    private void updateBetText()
    {
        betText.text = currencyFormatter.format(currentValue);
    }
}
