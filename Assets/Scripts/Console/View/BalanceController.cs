using System;
using UnityEngine;
using TMPro;

/// <remarks>
/// I'm struggling with another aspect of my same old Unity problem with good dependency
/// injection practices.  I *could* add a bunch of null checks and handled things like
/// disabling then re-enabling a behavior to properly manage event callbacks, but I hate
/// all that boilerplate.  So I keep looking for a better pattern to follow that will
/// ensure the functionality I want, with proper safety, without needless duplication or
/// extraction (like OnEnable and OnDisable seem perfect, except that I can't rely on the
/// presence of references in OnEnable).  So far I haven't found a solution I like, so for
/// now I'm going to keep the code clean and lean (albeit problematic) and expect that
/// references are a) populated during awake, and b) don't need to support dynamic change
/// unless that is explicitly part of my design goal for that class.
/// </remarks>
public class BalanceController : BaseProvider<IBalance>, IBalance
{
    public TMP_Text balanceText;

    public ICurrencyFormatter currencyFormatter { get; set; }
    public IWallet wallet { get; set; }

    public override IBalance value => this;

    private void Start()
    {
        wallet.onBalanceChanged += handleBalanceChanged;

        updateBalance();
    }

    private void handleBalanceChanged(object sender, EventArgs e)
    {
        updateBalance();
    }

    private void updateBalance()
    {
        balanceText.text = currencyFormatter.format(wallet.balance);
    }
}
