using System;

public interface IBankingDialog
{
    bool areFundsPending { get; }
    event EventHandler<AmountEventArgs> onDepositRequested;
    event EventHandler onCashoutRequested;
    event EventHandler onDialogHidden;

    void show();
    void hide();
}
