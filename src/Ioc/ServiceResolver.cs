namespace Ioc;

internal class ServiceResolver(IDictionary<Type, (RegistrationPolicy,Func<IServiceResolver, object>?)> types) : IServiceResolver
{
    private readonly IDictionary<Type, (RegistrationPolicy policy,Func<IServiceResolver, object>? generate)> _typesMap = types;
    private readonly Dictionary<Type, object> _instances = [];

    public T GetRequiredService<T>() where T : class
    {
        return (T)GetRequiredService(typeof(T));
    }

    public T GetRequiredService<T>(T t) where T : class
    {
        return GetRequiredService<T>();
    }

    public T? GetService<T>() where T : class
    {
        return (T?)GetService(typeof(T));
    }

    public object? GetService(Type type)
    {
        try
        {
            return GetRequiredService(type);
        }
        catch (NullReferenceException)
        {
            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private object GetRequiredService(Type type)
    {
        if (!_typesMap.TryGetValue(type, out var value))
            throw new NullReferenceException($"Cannot resolve instance of type {type}");

        var policy = value;
        if (policy.policy == RegistrationPolicy.Transient)
        {
            if (policy.generate is null)
                return CreateInstance(type);
            else
                return policy.generate(this);
        }

        if (_instances.TryGetValue(type, out var instance))
        {
            return instance;
        }

        instance = policy.generate is null ? CreateInstance(type) : policy.generate(this);
        _instances[type] = instance!;
        return instance!;
    }

    private object CreateInstance(Type type)
    {
        if (!type.IsClass)
        {
            return default!;
        }

        var ctor = type.GetConstructors()[0];
        var @params = ctor
            .GetParameters()
            .Select(p => p.ParameterType)
            .Select(t => GetService(t) ?? CreateInstance(t))
            .ToArray();

        if (@params.Length == 0)
            return Activator.CreateInstance(type)!;

        return Activator.CreateInstance(type, @params)!;
    }
}
