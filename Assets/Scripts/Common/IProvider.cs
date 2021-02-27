public interface IProvider<out T>
{
    T value { get; }
}
