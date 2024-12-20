namespace Ioc;

public interface IServiceContainer
{
    void AddSingleton<TService>(TService instance) where TService : class;
    void AddSingleton<TService>() where TService : class;
    void AddSingleton<TService, TImplementation>() where TImplementation : TService where TService : class;
    void AddSingleton<TService>(TService instance, Func<IServiceResolver, TService> factory) where TService : class;
    void AddSingleton<TService>(Func<IServiceResolver, TService> factory) where TService : class;
    void AddSingleton<TService, TImplementation>(Func<IServiceResolver, TService> factory) where TImplementation : TService where TService : class;


    void AddTransient<TService>() where TService : class;
    void AddTransient<TService>(TService instance) where TService : class;
    void AddTransient<TService, TImplementation>() where TImplementation : TService;

    void AddScoped<TService>(TService instance) where TService : class;
    void AddScoped<TService>() where TService : class;
    void AddScoped<TService, TImplementation>() where TImplementation : TService where TService : class;
    void AddScoped<TService>(TService instance, Func<IServiceResolver, TService> factory) where TService : class;
    void AddScoped<TService>(Func<IServiceResolver, TService> factory) where TService : class;
    void AddScoped<TService, TImplementation>(Func<IServiceResolver, TService> factory) where TImplementation : TService where TService : class;



    IServiceResolver GetProvider();
    IServiceResolver GetProvider(IServiceScope scope);
    ServiceScope CreateScope();
}
