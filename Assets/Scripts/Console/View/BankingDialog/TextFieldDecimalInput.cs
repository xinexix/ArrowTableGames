using System;
using System.Globalization;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class TextFieldDecimalInput : BaseProvider<IDecimalInput>, IDecimalInput
{
    private TMP_InputField _inputField;
    private float _value = 0;

    public float inputValue => _value;

    public event EventHandler onValueChanged;

    public override IDecimalInput value => this;

    private void Start()
    {
        _inputField = GetComponent<TMP_InputField>();

        _inputField.onValueChanged.AddListener(updateInput);

        resetValue();
    }

    private void updateInput(string input)
    {
        var parsedValue = float.Parse(input, CultureInfo.InvariantCulture);

        if (parsedValue > 0)
        {
            updateValue(parsedValue);
        }
        else
        {
            resetValue();
        }
    }

    public void resetValue()
    {
        updateValue(0f);

        _inputField.SetTextWithoutNotify("");
    }

    private void updateValue(float value)
    {
        if (Mathf.Approximately(value, _value)) return;

        _value = value;

        onValueChanged?.Invoke(this, EventArgs.Empty);
    }
}
