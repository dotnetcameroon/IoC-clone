namespace Ioc;
public partial class ServiceContainer : IServiceContainer
{
    private readonly Dictionary<Type, (RegistrationPolicy policy, Func<IServiceResolver, object>? factory)> _typesMap = [];

    public ServiceScope CreateScope()
    {
        return new ServiceScope(this);
    }

    public IServiceResolver GetProvider()
    {
        return new ServiceResolver(_typesMap);
    }

    public IServiceResolver GetProvider(IServiceScope scope)
    {
        return new ServiceResolver(_typesMap, scope);
    }
}
