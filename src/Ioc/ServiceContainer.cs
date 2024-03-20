namespace Ioc;
public class ServiceContainer : IServiceContainer
{
    private readonly Dictionary<Type, RegistrationPolicy> _types = [];
    public void AddSingleton<TService>(TService instance) where TService : class
    {
        _types.Add(typeof(TService), RegistrationPolicy.Singleton);
    }

    public void AddSingleton<TService>()
    {
        _types.Add(typeof(TService), RegistrationPolicy.Singleton);
    }

    public void AddSingleton<TService, TImplementation>() where TImplementation : TService
    {
        _types.Add(typeof(TService), RegistrationPolicy.Singleton);
    }

    public IServiceResolver GetProvider()
    {
        return new ServiceResolver(_types);
    }
}
