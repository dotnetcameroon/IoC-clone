namespace Ioc;
public partial class ServiceContainer : IServiceContainer
{
    public void AddScoped<TService>(TService instance) where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Scoped, null));
    }

    public void AddScoped<TService>() where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Scoped, null));
    }

    public void AddScoped<TService, TImplementation>() where TImplementation : TService where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Scoped, null));
    }

    public void AddScoped<TService>(TService instance, Func<IServiceResolver, TService> generate) where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Scoped, generate));
    }

    public void AddScoped<TService>(Func<IServiceResolver, TService> generate) where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Scoped, generate));
    }

    public void AddScoped<TService, TImplementation>(Func<IServiceResolver, TService> generate) where TImplementation : TService where TService : class
    {
        _types.Add(typeof(TService), (RegistrationPolicy.Scoped, generate));
    }
}
