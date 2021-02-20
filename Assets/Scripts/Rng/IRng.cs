/// <remarks>
/// There are multiple RNG options available in C# (such as System.Random and
/// System.Security.Cryptography.RNGCryptoServiceProvider).  This interface serves as an
/// adapter for using an RNG within this project
/// </remarks>
public interface IRng
{
    void setSeed(int seedValue);
    int randomIntRange(int min, int max);
    double randomDouble();
}
