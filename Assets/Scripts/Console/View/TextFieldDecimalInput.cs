using System;
using System.Globalization;
using UnityEngine;
using TMPro;

public class TextFieldDecimalInput : BaseDecimalInput
{
    private float _value = 0;

    public override float value => _value;

    public override event EventHandler onValueChanged;

    public TMP_InputField inputField;

    private void OnEnable()
    {
        inputField?.onValueChanged.AddListener(updateInput);
    }

    private void OnDisable()
    {
        inputField?.onValueChanged.RemoveListener(updateInput);
    }

    private void updateInput(string input)
    {
        var parsedValue = float.Parse(input, CultureInfo.InvariantCulture);

        updateValue(Math.Max(parsedValue, 0f));
    }

    public override void resetValue()
    {
        updateValue(0f);
    }

    private void updateValue(float value)
    {
        if (Mathf.Approximately(value, _value)) return;

        _value = value;

        onValueChanged?.Invoke(this, EventArgs.Empty);
    }
}
