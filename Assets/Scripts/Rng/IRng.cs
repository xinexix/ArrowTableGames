/// <remarks>
/// There are multiple RNG options available in C# (such as System.Random and
/// System.Security.Cryptography.RNGCryptoServiceProvider).  This interface serves as an
/// adapter for using an RNG within this project
/// </remarks>
public interface IRng
{
    void setSeed(int? seedValue);
    int randomInRange(int min, int max);
    int randomInt();
    double randomDouble();
}
