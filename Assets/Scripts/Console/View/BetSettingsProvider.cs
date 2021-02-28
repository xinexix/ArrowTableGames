using UnityEngine;

[RequireComponent(typeof(DenomProvider))]
public class BetSettingsProvider : BaseProvider<IBetSettingsController>
{
    private IBetSettingsController _betSettings;

    public override IBetSettingsController value => _betSettings;

    private void Awake()
    {
        var denomProvider = GetComponent<DenomProvider>();

        _betSettings = new BetSettings(denomProvider.value);
    }
}
