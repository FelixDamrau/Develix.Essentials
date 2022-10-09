namespace Develix.Essentials.Core.Tests;

public class ResultTests
{
    [Fact]
    public void ValidGenericResult()
    {
        var value = 5;

        var result = Result.Ok(value);

        result.Value.Should().Be(value);
        result.ValueOrDefault.Should().Be(value);
        result.Valid.Should().BeTrue();
        result.Message.Should().Be(default);
    }

    [Fact]
    public void InvalidGenericResult()
    {
        var message = "Fail";

        var result = Result.Fail<int>(message);

        result.ValueOrDefault.Should().Be(default);
        result.Valid.Should().BeFalse();
        result.Message.Should().Be(message);
    }

    [Fact]
    public void InvalidGenericResultThrowsOnGetValue()
    {
        var message = "Fail";

        var result = Result.Fail<int>(message);

        var exception = Record.Exception(() => result.Value);

        Assert.IsType<InvalidOperationException>(exception);
    }

    [Fact]
    public void ValidVoidResult()
    {
        var result = Result.Ok();

        result.Valid.Should().BeTrue();
        result.Message.Should().Be(default);
    }

    [Fact]
    public void InvalidVoidResult()
    {
        var message = "Fail";

        var result = Result.Fail(message);

        result.Valid.Should().BeFalse();
        result.Message.Should().Be(message);
    }

    [Fact]
    public void CovariantResultInterface()
    {
        var result = Result.Ok(new FooDerived());

        result.Should().BeAssignableTo<IResult<Foo>>();
    }

    private class Foo { }
    private class FooDerived : Foo { }
}
