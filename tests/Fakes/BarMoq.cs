namespace tests.Fakes;

public class BarMoq(Moq moq, FooMoq foo, int number)
{
    public FooMoq Foo { get; set; } = foo;
    public int Number { get; set; } = number;
    public Moq Moq { get; set; } = moq;
}
