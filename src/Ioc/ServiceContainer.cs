namespace Ioc;
public class ServiceContainer : IServiceContainer
{
    private readonly Dictionary<Type, (RegistrationPolicy,Func<IServiceResolver, object>?)> _types = [];
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
        _types.Add(typeof(TService), (RegistrationPolicy.Transient, generate));
    }

    public void AddSingleton<TService>(Func<IServiceResolver, TService> generate) where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Transient, generate));
    }

    public void AddSingleton<TService, TImplementation>(Func<IServiceResolver, TService> generate) where TImplementation : TService  where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Transient, generate));
    }

    public void AddTransient<TService>() where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Transient, null));
    }

    public void AddTransient<TService>(TService instance) where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Transient, null));
    }

    public void AddTransient<TService, TImplementation>() where TImplementation : TService
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Transient, null));
    }

    public IServiceResolver GetProvider()
    {
        return new ServiceResolver(_types);
    }
}
