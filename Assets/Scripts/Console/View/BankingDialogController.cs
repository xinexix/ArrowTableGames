using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextFieldDecimalInput))]
public class BankingDialogController : BaseProvider<IBankingDialog>, IBankingDialog
{
    private IDecimalInput _depositInput;
    private ICurrencyFormatter _formatter;

    public TMP_Text denomText;

    public ICurrencyFormatter currencyFormatter
    {
        get => _formatter;
        set
        {
            _formatter = value;
        }
    }

    public bool isShowing => gameObject.activeSelf;

    public bool isDepositPending => !Mathf.Approximately(_depositInput.value, 0f);

    public event EventHandler<AmountEventArgs> onDepositRequested;

    public event EventHandler onCashoutRequested;

    public event EventHandler onDialogHidden;

    public override IBankingDialog value => this;

    private void Awake()
    {
        _depositInput = GetComponent<IDecimalInput>();

        hide();
    }

    public void show()
    {
        if (isShowing) return;

        if (denomText != null)
        {
            denomText.text = _formatter.format(1);
        }

        gameObject.SetActive(true);
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
        var amount = _depositInput.value;
        if (Mathf.Approximately(amount, 0f)) return;

        onDepositRequested?.Invoke(this, new AmountEventArgs(amount));

        _depositInput.resetValue();
    }

    public void acceptWithdrawal()
    {
        onCashoutRequested?.Invoke(this, EventArgs.Empty);

        _depositInput.resetValue();
    }
}
