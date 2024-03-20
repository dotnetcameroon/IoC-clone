using Ioc;

namespace tests;

public class Tests
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
}

public class Moq
{
    public Guid Id { get; } = Guid.NewGuid();
}

public class FooMoq(Moq moq)
{
    public Moq Moq { get; set; } = moq;
}

public class BarMoq(Moq moq, FooMoq foo, int number)
{
    public FooMoq Foo { get; set; } = foo;
    public int Number { get; set; } = number;
    public Moq Moq { get; set; } = moq;
}
