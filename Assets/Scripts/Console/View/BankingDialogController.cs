using System;
using UnityEngine;

public class BankingDialogController : MonoBehaviour, IBankingDialog
{
    public IProvider<IWalletController> walletProvider;

    public BaseFundAmount fundValue;

    public bool areFundsPending => false;

    public event EventHandler<AmountEventArgs> onDepositRequested;

    public event EventHandler onCashoutRequested;

    public event EventHandler onDialogHidden;

    private void Start()
    {
    }

    public void show()
    {

    }

    public void hide()
    {

    }
}
