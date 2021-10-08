namespace Develix.Essentials.Core;

public static class Result
{
    public static Result<T> Ok<T>(T value) => new(true, value, default);

    public static Result<T> Fail<T>(string message) => new(false, default, message);
}
