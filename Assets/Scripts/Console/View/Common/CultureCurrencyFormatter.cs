using System.Globalization;

public class CultureCurrencyFormatter : ICurrencyFormatter
{
    private float _denomination;
    private CultureInfo _culture;

    public CultureCurrencyFormatter(float denomination, string cultureName)
    {
        _denomination = denomination;
        _culture = CultureInfo.CreateSpecificCulture(cultureName);
    }

    public string format(int value)
    {
        var amount = value * _denomination;
        return amount.ToString("C", _culture);
    }
}
