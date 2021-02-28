using UnityEngine;

[RequireComponent(typeof(DenomProvider))]
public class WalletProvider : BaseProvider<IWalletController>
{
    private IWalletController _wallet;

    public override IWalletController value => _wallet;

    private void Awake()
    {
        var denomProvider = GetComponent<DenomProvider>();

        _wallet = new Wallet(denomProvider.value);
    }
}
