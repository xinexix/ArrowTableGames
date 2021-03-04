using UnityEngine;

[CreateAssetMenu(menuName = "Script Objects/Wallet Model")]
public class WalletSO : BaseSOProvider<IWalletController>
{
    private IWalletController _wallet;

    public BaseSOProvider<float> denomination;

    public override IWalletController value
    {
        get
        {
            if (_wallet == null)
            {
                _wallet = new Wallet(denomination.value);
            }

            return _wallet;
        }
    }
}
