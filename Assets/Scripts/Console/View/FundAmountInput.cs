using System;
using TMPro;
using UnityEngine;

public class FundAmountInput : BaseFundAmount
{
    private float _amount = 0;

    public override float amount => _amount;

    public override event EventHandler onAmountChanged;

    public void updateInput(TMP_Text inputText)
    {
        var input = inputText.text;

        Debug.Log("TEXT = " + input);
    }

    public override void clearAmount()
    {
        updateAmount(0f);
    }

    private void updateAmount(float value)
    {
        _amount = Math.Max(value, 0f);

        onAmountChanged?.Invoke(this, EventArgs.Empty);
    }
}
