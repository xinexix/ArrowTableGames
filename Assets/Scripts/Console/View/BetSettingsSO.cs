using UnityEngine;

[CreateAssetMenu(menuName = "Script Objects/Bet Settings Model")]
public class BetSettingsSO : BaseSOProvider<IBetSettingsController>
{
    public BaseSOProvider<float> denomination;

    private IBetSettingsController _betSettings;

    public override IBetSettingsController value
    {
        get
        {
            if (_betSettings == null)
            {
                _betSettings = new BetSettings(denomination.value);
            }

            return _betSettings;
        }
    }
}
