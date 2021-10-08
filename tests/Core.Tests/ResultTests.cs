using Develix.Essentials.Core;

namespace Core.Tests;

public class ResultTests
{
    [Fact]
    public void ValidResult()
    {
        var value = new Foo();
        var result = Result.Ok(value);

        result.Value.Should().Be(value);
            result.ValueOrDefault.Should().Be(value);
        result.Valid.Should().BeTrue();
        result.Message.Should().Be(default);
    }

    [Fact]
    public void InvalidResult()
    {
        var message = "Fail";
        var result = Result.Fail<Foo>(message);

        result.ValueOrDefault.Should().Be(default);
        result.Valid.Should().BeFalse();
        if (!result.Valid)
        result.Message.Should().Be(message);
    }

    [Fact]
    public void InvalidResultThrowsOnGetValue()
    {
        var message = "Fail";
        var result = Result.Fail<int>(message);

        var exception = Record.Exception(() => result.Value);

        Assert.IsType<InvalidOperationException>(exception);
    }

    class Foo { }
}
