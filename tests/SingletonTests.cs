using Ioc;
using tests.Fakes;

namespace tests;

public class SingletonTests
{
    [Fact]
    public void AddSingleton_RegistersAnInstanceOfTheRequestedService()
    {
        IServiceContainer collection = new ServiceContainer();
        collection.AddSingleton<Moq>();

        var provider = collection.GetProvider();
        var instance = provider.GetRequiredService<Moq>();

        Assert.NotNull(instance);
        Assert.IsType<Moq>(instance);
    }

    [Fact]
    public void AddSingleton_ThrowsNullRefExceptionWhenTheServiceIsNotRegistered()
    {
        IServiceContainer collection = new ServiceContainer();

        var provider = collection.GetProvider();
        Assert.Throws<NullReferenceException>(() => _ = provider.GetRequiredService<Moq>());
    }

    [Fact]
    public void AddSingleton_RegistersASingleInstanceOfTheRequestedService()
    {
        IServiceContainer collection = new ServiceContainer();
        collection.AddSingleton<Moq>();

        var provider = collection.GetProvider();
        var instance = provider.GetRequiredService<Moq>();
        var instance2 = provider.GetRequiredService<Moq>();

        Assert.Equal(instance, instance2);
        Assert.Equal(instance.Id, instance2.Id);
    }

    [Fact]
    public void GetRequiredService_ReturnsAnInstanceOfTheRequestedServiceWithItsDependencies()
    {
        IServiceContainer collection = new ServiceContainer();
        collection.AddSingleton<FooMoq>();
        collection.AddSingleton<BarMoq>();
        collection.AddSingleton<Moq>();

        var provider = collection.GetProvider();
        var foo = provider.GetRequiredService<FooMoq>();
        var bar = provider.GetRequiredService<BarMoq>();
        var moq = provider.GetRequiredService<Moq>();

        Assert.Equal(moq, foo.Moq);
        Assert.Equal(moq, bar.Moq);
        Assert.Equal(moq, bar.Foo.Moq);
        Assert.Equal(moq.Id, foo.Moq.Id);
        Assert.Equal(moq.Id, bar.Moq.Id);
        Assert.Equal(moq.Id, bar.Foo.Moq.Id);
    }

    [Fact]
    public void GetRequiredService_WhitGenerateDelegateReturnsAnInstanceOfTheRequestedServiceWithItsDependencies()
    {
        IServiceContainer collection = new ServiceContainer();
        collection.AddSingleton<Moq>();
        collection.AddSingleton(sr =>
        {
            var moq = sr.GetRequiredService<Moq>();
            var foo = sr.GetRequiredService<FooMoq>();
            return new BarMoq(moq, foo, 100);
        });
        collection.AddSingleton(sr =>
        {
            var moq = sr.GetRequiredService<Moq>();
            return new FooMoq(moq);
        });

        var provider = collection.GetProvider();
        var foo = provider.GetRequiredService<FooMoq>();
        var bar = provider.GetRequiredService<BarMoq>();
        var moq = provider.GetRequiredService<Moq>();

        Assert.Equal(100, bar.Number);
    }
}
