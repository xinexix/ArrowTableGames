public class NullCurrencyFormatter : ICurrencyFormatter
{
    private static ICurrencyFormatter _instance = new NullCurrencyFormatter();

    public static ICurrencyFormatter instance => _instance;

    public string format(int value)
    {
        return value.ToString();
    }
}
