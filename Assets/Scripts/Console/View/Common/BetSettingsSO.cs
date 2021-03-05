using UnityEngine;

[CreateAssetMenu(menuName = "Script Objects/Bet Settings Model")]
public class BetSettingsSO : BaseSOProvider<IBetSettingsController>
{
    private IBetSettingsController _betSettings;

    public BaseSOProvider<float> denomination;

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
