namespace Ioc;

public interface IServiceResolver
{
    T? GetService<T>() where T : class;
    T GetRequiredService<T>() where T : class;
}
