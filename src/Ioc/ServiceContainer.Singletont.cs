namespace Ioc;
public partial class ServiceContainer : IServiceContainer
{
    public void AddSingleton<TService>(TService instance) where TService : class
    {
        _typesMap.Add(typeof(TService), (RegistrationPolicy.Singleton, null));
    }

    public void AddSingleton<TService>() where TService : class
    {
        _typesMap.Add(typeof(TService), (RegistrationPolicy.Singleton, null));
    }

    public void AddSingleton<TService, TImplementation>() where TImplementation : TService where TService : class
    {
        _typesMap.Add(typeof(TService), (RegistrationPolicy.Singleton, null));
    }

    public void AddSingleton<TService>(TService instance, Func<IServiceResolver, TService> factory) where TService : class
    {
        _typesMap.Add(typeof(TService), (RegistrationPolicy.Singleton, factory));
    }

    public void AddSingleton<TService>(Func<IServiceResolver, TService> factory) where TService : class
    {
        _typesMap.Add(typeof(TService), (RegistrationPolicy.Singleton, factory));
    }

    public void AddSingleton<TService, TImplementation>(Func<IServiceResolver, TService> factory) where TImplementation : TService where TService : class
    {
        _typesMap.Add(typeof(TService), (RegistrationPolicy.Singleton, factory));
    }
}
