using UnityEngine;

[CreateAssetMenu(menuName = "Script Objects/Wallet Model")]
public class WalletSO : BaseSOProvider<IWalletController>
{
    public BaseSOProvider<float> denomination;

    private IWalletController _wallet;

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
