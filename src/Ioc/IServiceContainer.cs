namespace Ioc;

public interface IServiceContainer
{
    void AddSingleton<TService>(TService instance) where TService : class;
    void AddSingleton<TService>() where TService : class;
    void AddSingleton<TService, TImplementation>() where TImplementation : TService where TService : class;
    void AddSingleton<TService>(TService instance, Func<IServiceResolver, TService> generate) where TService : class;
    void AddSingleton<TService>(Func<IServiceResolver, TService> generate) where TService : class;
    void AddSingleton<TService, TImplementation>(Func<IServiceResolver, TService> generate) where TImplementation : TService  where TService : class;


    void AddTransient<TService>() where TService : class;
    void AddTransient<TService>(TService instance) where TService : class;
    void AddTransient<TService, TImplementation>() where TImplementation : TService;


    IServiceResolver GetProvider();
}
