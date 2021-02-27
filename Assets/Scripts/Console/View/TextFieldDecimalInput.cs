using System;
using TMPro;
using UnityEngine;

public class TextFieldDecimalInput : BaseDecimalInput
{
    private float _value = 0;

    public override float value => _value;

    public override event EventHandler onValueChanged;

    public void updateInput(TMP_Text inputText)
    {
        var input = inputText.text;

        inputText.text = "007";

        Debug.Log("TEXT = " + input);
    }

    public override void resetValue()
    {
        updateValue(0f);
    }

    private void updateValue(float value)
    {
        _value = Math.Max(value, 0f);

        onValueChanged?.Invoke(this, EventArgs.Empty);
    }
}
