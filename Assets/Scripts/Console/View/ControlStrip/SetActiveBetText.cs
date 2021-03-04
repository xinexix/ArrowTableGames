using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class SetActiveBetText : MonoBehaviour
{
    private ICurrencyFormatter _formatter;
    private IBetSettings _betSettings;
    private TMP_Text _activeBetText;

    public BaseSOProvider<ICurrencyFormatter> currencyFormatterProvider;
    // TODO hopefully fix IBetSettingsController to just IBetSettings
    public BaseSOProvider<IBetSettingsController> betSettingsProvider;

    private void Start()
    {
        _formatter = currencyFormatterProvider.value;
        _betSettings = betSettingsProvider.value;

        _activeBetText = GetComponent<TMP_Text>();

        _betSettings.onActiveBetChanged += handleActiveBetChanged;

        updateText();
    }

    private void handleActiveBetChanged(object sender, EventArgs e)
    {
        updateText();
    }

    private void updateText()
    {
        _activeBetText.text = _formatter.format(_betSettings.activeBet);
    }
}
