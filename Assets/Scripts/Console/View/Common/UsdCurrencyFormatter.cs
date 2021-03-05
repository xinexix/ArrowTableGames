public class UsdCurrencyFormatter : ICurrencyFormatter
{
    private float _denomination;

    public UsdCurrencyFormatter(float denomination)
    {
        _denomination = denomination;
    }

    public string format(int value)
    {
        var amount = value * _denomination;
        return amount.ToString("C");
    }
}
