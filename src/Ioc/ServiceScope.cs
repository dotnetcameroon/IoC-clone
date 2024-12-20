namespace Ioc;
public class ServiceScope : IServiceScope
{
    internal ServiceScope(){}

    private readonly Dictionary<Type, object> _instances = [];

    public void AddService(Type type, object scopedInstance)
    {
        _instances[type] = scopedInstance;
    }

    public object? GetService(Type type)
    {
        if (_instances.TryGetValue(type, out var instance))
            return instance;
        return null;
    }
}
