public interface IWalletController : IWallet
{
    void setCredit(int value);
    void adjustCredit(int offset);

    void setWager(int value);
    void adjustWager(int offset);
    void resolveWager();
}
