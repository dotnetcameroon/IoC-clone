using Ioc;
using tests.Fakes;

namespace tests;

public class TransientTests
{
    [Fact]
    public void AddTransient_RegistersAnInstanceOfTheRequestedService()
    {
        IServiceContainer collection = new ServiceContainer();
        collection.AddTransient<Moq>();

        var provider = collection.GetProvider();
        var instance = provider.GetRequiredService<Moq>();

        Assert.NotNull(instance);
        Assert.IsType<Moq>(instance);
    }

    [Fact]
    public void AddTransient_RegistersANewInstanceEachTimeItIsRequested()
    {
        IServiceContainer collection = new ServiceContainer();
        collection.AddTransient<Moq>();

        var provider = collection.GetProvider();
        var instance = provider.GetRequiredService<Moq>();
        var instance2 = provider.GetRequiredService<Moq>();

        Assert.NotEqual(instance, instance2);
        Assert.NotEqual(instance.Id, instance2.Id);
    }

    [Fact]
    public void GetRequiredService_ReturnsANewInstanceEachTimeItIsRequested()
    {
        IServiceContainer collection = new ServiceContainer();
        collection.AddTransient<FooMoq>();
        collection.AddTransient<BarMoq>();
        collection.AddTransient<Moq>();

        var provider = collection.GetProvider();
        var foo = provider.GetRequiredService<FooMoq>();
        var bar = provider.GetRequiredService<BarMoq>();
        var moq = provider.GetRequiredService<Moq>();

        Assert.NotEqual(moq, foo.Moq);
        Assert.NotEqual(moq, bar.Moq);
        Assert.NotEqual(moq, bar.Foo.Moq);
        Assert.NotEqual(moq.Id, foo.Moq.Id);
        Assert.NotEqual(moq.Id, bar.Moq.Id);
        Assert.NotEqual(moq.Id, bar.Foo.Moq.Id);
    }
}
