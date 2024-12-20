namespace Ioc;
public class ServiceScope : IServiceScope
{
    private readonly Dictionary<Type, object> _instances = [];
    private readonly IServiceContainer _serviceContainer;

    internal ServiceScope(IServiceContainer serviceContainer)
    {
        _serviceContainer = serviceContainer;
    }

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

    public IServiceResolver GetProvider()
    {
        return _serviceContainer.GetProvider(this);
    }
}
