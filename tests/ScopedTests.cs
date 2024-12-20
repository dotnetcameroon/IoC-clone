using Ioc;
using tests.Fakes;

namespace tests;

public class ScopedTests
{
    [Fact]
    public void AddScoped_RegistersAnInstanceOfTheRequestedService()
    {
        IServiceContainer collection = new ServiceContainer();
        collection.AddScoped<Moq>();
        var scope = collection.CreateScope();
        var provider = scope.GetProvider();
        var instance = provider.GetRequiredService<Moq>();

        Assert.NotNull(instance);
        Assert.IsType<Moq>(instance);
    }

    [Fact]
    public void AddScoped_ThrowsNullRefExceptionWhenTheServiceIsNotRegistered()
    {
        IServiceContainer collection = new ServiceContainer();
        var scope = collection.CreateScope();
        var provider = scope.GetProvider();
        Assert.Throws<NullReferenceException>(() => _ = provider.GetRequiredService<Moq>());
    }

    [Fact]
    public void AddScoped_RegistersASingleInstanceOfTheRequestedServicePerScope()
    {
        IServiceContainer collection = new ServiceContainer();
        collection.AddScoped<Moq>();

        var scope = collection.CreateScope();
        var provider = scope.GetProvider();
        var instance = provider.GetRequiredService<Moq>();
        var instance2 = provider.GetRequiredService<Moq>();

        Assert.Equal(instance, instance2);
        Assert.Equal(instance.Id, instance2.Id);
    }

    [Fact]
    public void AddScoped_RegistersDifferentInstanceOfTheRequestedServicePerScope()
    {
        IServiceContainer collection = new ServiceContainer();
        collection.AddScoped<Moq>();

        var scope1 = collection.CreateScope();
        var provider1 = scope1.GetProvider();

        var scope2 = collection.CreateScope();
        var provider2 = scope2.GetProvider();

        var instance = provider1.GetRequiredService<Moq>();
        var instance2 = provider2.GetRequiredService<Moq>();

        Assert.NotEqual(instance, instance2);
        Assert.NotEqual(instance.Id, instance2.Id);
    }

    [Fact]
    public void GetRequiredService_ReturnsAnInstanceOfTheRequestedServiceWithItsDependencies()
    {
        IServiceContainer collection = new ServiceContainer();
        collection.AddSingleton<FooMoq>();
        collection.AddScoped<BarMoq>();
        collection.AddScoped<Moq>();

        var scope = collection.CreateScope();
        var provider = scope.GetProvider();
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
    public void GetRequiredService_ThrowsExceptionWhenCalledWithoutScope()
    {
        IServiceContainer collection = new ServiceContainer();
        collection.AddScoped<Moq>();
        var provider = collection.GetProvider();
        Assert.Throws<InvalidOperationException>(() => _ = provider.GetRequiredService<Moq>());
    }
}
