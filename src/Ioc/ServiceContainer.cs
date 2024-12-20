namespace Ioc;
public partial class ServiceContainer : IServiceContainer
{
    private readonly Dictionary<Type, (RegistrationPolicy,Func<IServiceResolver, object>?)> _types = [];

    public IServiceResolver GetProvider()
    {
        return new ServiceResolver(_types);
    }
}
