using System;

public interface IBankingDialog
{
    ICurrencyFormatter currencyFormatter { get; set; }

    bool isShowing { get; }
    bool isDepositPending { get; }
    event EventHandler<AmountEventArgs> onDepositRequested;
    event EventHandler onCashoutRequested;
    event EventHandler onDialogHidden;

    void show();
    void hide();
}
