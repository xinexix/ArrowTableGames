using System;
using UnityEngine;
using TMPro;

public class BankingDialogController : BaseProvider<IBankingDialog>, IBankingDialog
{
    private ICurrencyFormatter _formatter;
    private IDecimalInput _depositInput;

    public BaseSOProvider<ICurrencyFormatter> currencyFormatterProvider;
    public BaseProvider<IDecimalInput> depositInputProvider;

    public bool isShowing => gameObject.activeSelf;

    public bool isDepositPending => !Mathf.Approximately(_depositInput.inputValue, 0f);

    public event EventHandler<AmountEventArgs> onDepositRequested;

    public event EventHandler onCashoutRequested;

    public event EventHandler onDialogHidden;

    public override IBankingDialog value => this;

    private void Start()
    {
        _formatter = currencyFormatterProvider.value;
        _depositInput = depositInputProvider.value;

        hide();
    }

    public void show()
    {
        if (isShowing) return;

        gameObject.SetActive(true);

        _depositInput.resetValue();
    }

    public void hide()
    {
        if (!isShowing) return;

        gameObject.SetActive(false);

        _depositInput.resetValue();

        onDialogHidden?.Invoke(this, EventArgs.Empty);
    }

    public void acceptDeposit()
    {
        var amount = _depositInput.inputValue;
        if (Mathf.Approximately(amount, 0f)) return;

        onDepositRequested?.Invoke(this, new AmountEventArgs(amount));

        _depositInput.resetValue();
    }

    public void acceptCashOut()
    {
        onCashoutRequested?.Invoke(this, EventArgs.Empty);

        _depositInput.resetValue();
    }
}
