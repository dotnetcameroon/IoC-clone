namespace Ioc;
public partial class ServiceContainer : IServiceContainer
{
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
}
