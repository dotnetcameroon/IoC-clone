namespace Ioc;

public interface IServiceContainer
{
    void AddSingleton<TService>(TService instance) where TService : class;
    void AddSingleton<TService>();
    void AddSingleton<TService, TImplementation>() where TImplementation : TService;
    IServiceResolver GetProvider();
}
