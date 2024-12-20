namespace Ioc;
public interface IServiceScope
{
    void AddService(Type type, object scopedInstance);
    object? GetService(Type type);
}
