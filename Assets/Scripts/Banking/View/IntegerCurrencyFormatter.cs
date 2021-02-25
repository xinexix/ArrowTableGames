public class IntegerCurrencyFormatter : ICurrencyFormatter
{
    private static ICurrencyFormatter _instance = new IntegerCurrencyFormatter();

    public static ICurrencyFormatter instance => _instance;

    public string format(int value)
    {
        return value.ToString();
    }
}
