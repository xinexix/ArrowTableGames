using UnityEngine;

[CreateAssetMenu(menuName = "Script Objects/Currency Formatter")]
public class CurrencyFormatterSO : ScriptableObject
{
    private ICurrencyFormatter _formatter;

    public FloatSO denomination;
    public string cultureName;

    public ICurrencyFormatter formatter => _formatter;

    public void OnEnable()
    {
        _formatter = new CultureCurrencyFormatter(denomination.value, cultureName);
    }
}
