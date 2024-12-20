namespace Ioc;
public partial class ServiceContainer : IServiceContainer
{
    public void AddSingleton<TService>(TService instance) where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Singleton, null));
    }

    public void AddSingleton<TService>() where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Singleton, null));
    }

    public void AddSingleton<TService, TImplementation>() where TImplementation : TService where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Singleton, null));
    }

    public void AddSingleton<TService>(TService instance, Func<IServiceResolver, TService> generate) where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Singleton, generate));
    }

    public void AddSingleton<TService>(Func<IServiceResolver, TService> generate) where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Singleton, generate));
    }

    public void AddSingleton<TService, TImplementation>(Func<IServiceResolver, TService> generate) where TImplementation : TService  where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Singleton, generate));
    }
}
