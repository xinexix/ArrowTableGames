using System;
using UnityEngine;

[RequireComponent(typeof(TextFieldDecimalInput))]
public class BankingDialogController : MonoBehaviour, IBankingDialog
{
    public IProvider<IWalletController> walletProvider;

    private IDecimalInput _depositInput;

    public bool areFundsPending => false;

    public event EventHandler<AmountEventArgs> onDepositRequested;

    public event EventHandler onCashoutRequested;

    public event EventHandler onDialogHidden;

    private void Start()
    {
        _depositInput = GetComponent<IDecimalInput>();
    }

    public void show()
    {

    }

    public void hide()
    {

    }

    public void acceptDeposit()
    {

    }

    public void acceptWithdrawal()
    {
        
    }
}
