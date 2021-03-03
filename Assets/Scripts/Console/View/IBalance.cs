public interface IBalance
{
    ICurrencyFormatter currencyFormatter { get; set; }
    IWallet wallet { get; set; }
}
