namespace Ioc;
public partial class ServiceContainer : IServiceContainer
{
    private readonly Dictionary<Type, (RegistrationPolicy,Func<IServiceResolver, object>?)> _types = [];

    public ServiceScope CreateScope()
    {
        return new ServiceScope(this);
    }

    public IServiceResolver GetProvider()
    {
        return new ServiceResolver(_types);
    }

    public IServiceResolver GetProvider(IServiceScope scope)
    {
        return new ServiceResolver(_types, scope);
    }
}
