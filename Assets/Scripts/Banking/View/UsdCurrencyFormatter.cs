using System.Globalization;

public class UsdCurrencyFormatter
{
    private float _denomination;
    private CultureInfo _culture;

    public UsdCurrencyFormatter(float denomination)
    {
        _denomination = denomination;
        _culture = CultureInfo.CreateSpecificCulture("en-US");
    }

    public string format(int value)
    {
        var amount = value * _denomination;
        return amount.ToString("C", _culture);
    }
}
