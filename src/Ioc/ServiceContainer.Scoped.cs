namespace Ioc;
public partial class ServiceContainer : IServiceContainer
{
    public void AddScoped<TService>(TService instance) where TService : class
    {
        _typesMap.Add(typeof(TService), (RegistrationPolicy.Scoped, null));
    }

    public void AddScoped<TService>() where TService : class
    {
        _typesMap.Add(typeof(TService), (RegistrationPolicy.Scoped, null));
    }

    public void AddScoped<TService, TImplementation>() where TImplementation : TService where TService : class
    {
        _typesMap.Add(typeof(TService), (RegistrationPolicy.Scoped, null));
    }

    public void AddScoped<TService>(TService instance, Func<IServiceResolver, TService> factory) where TService : class
    {
        _typesMap.Add(typeof(TService), (RegistrationPolicy.Scoped, factory));
    }

    public void AddScoped<TService>(Func<IServiceResolver, TService> factory) where TService : class
    {
        _typesMap.Add(typeof(TService), (RegistrationPolicy.Scoped, factory));
    }

    public void AddScoped<TService, TImplementation>(Func<IServiceResolver, TService> factory) where TImplementation : TService where TService : class
    {
        _typesMap.Add(typeof(TService), (RegistrationPolicy.Scoped, factory));
    }
}
