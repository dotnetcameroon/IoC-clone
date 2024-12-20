namespace Ioc;

internal class ServiceResolver(IDictionary<Type, (RegistrationPolicy, Func<IServiceResolver, object>?)> types) : IServiceResolver
{
    private readonly IDictionary<Type, (RegistrationPolicy policy,Func<IServiceResolver, object>? generate)> _typesMap = types;
    private readonly Dictionary<Type, object> _instances = [];
    private readonly IServiceScope? _scope;

    public ServiceResolver(
        IDictionary<Type, (RegistrationPolicy, Func<IServiceResolver, object>?)> types,
        IServiceScope scope) : this(types)
    {
        _scope = scope;
    }

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
        return policy.policy switch
        {
            RegistrationPolicy.Transient => RetrieveTransientInstance(policy, type),
            RegistrationPolicy.Scoped => RetrieveScopedInstance(policy, type),
            RegistrationPolicy.Singleton => RetrieveSingletonInstance(policy, type),
            _ => throw new NotSupportedException($"Policy {policy.policy} is not supported")
        };
    }

    private object RetrieveSingletonInstance((RegistrationPolicy policy, Func<IServiceResolver, object>? generate) policy, Type type)
    {
        /*
        Singleton
            If the instance is already created, return it
            else: Create a new instance and store it in the container
        */
        if (_instances.TryGetValue(type, out var instance))
        {
            return instance;
        }

        instance = policy.generate is null ? CreateInstance(type) : policy.generate(this);
        _instances[type] = instance!;
        return instance!;
    }

    private object RetrieveScopedInstance((RegistrationPolicy policy, Func<IServiceResolver, object>? generate) policy, Type type)
    {
        /*
        Scoped
            If the instance is already created inside the scope, return it
            else: Create a new instance and store it in the scope
        */
        if (_scope is null)
            throw new NullReferenceException("Cannot resolve scoped instance without a scope");

        var scopedInstance = _scope.GetService(type);
        if (scopedInstance is not null)
            return scopedInstance;

        scopedInstance = policy.generate is null ? CreateInstance(type) : policy.generate(this);
        _scope.AddService(type, scopedInstance);
        return scopedInstance;
    }

    private object RetrieveTransientInstance((RegistrationPolicy policy, Func<IServiceResolver, object>? generate) policy, Type type)
    {
        /*
        Transient
            Generate delegate ? use it
            else: Generate from the DI container
        */
        if (policy.generate is null)
            return CreateInstance(type);
        else
            return policy.generate(this);
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
