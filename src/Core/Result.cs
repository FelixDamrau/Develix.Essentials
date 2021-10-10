using System.Diagnostics.CodeAnalysis;

namespace Develix.Essentials.Core;

/// <summary>
/// This class represents a successful or failed evaluation of an action.
/// It also contains helper methods to create valid and failed results.
/// </summary>
public class Result : IResult
{
    /// <inheritdoc />
    [MemberNotNullWhen(false, nameof(Message))]
    public bool Valid { get; }

    /// <inheritdoc />
    public string? Message { get; }

    private Result(bool valid, string? message)
    {
        Message = message;
        Valid = valid;
    }

    /// <summary>
    /// Creates a new instance of a valid <see cref="Result{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Result{T}.Value"/> of the created result.</typeparam>
    /// <param name="value">The value of the created result.</param>
    public static Result<T> Ok<T>(T value) => new(true, value, default);

    /// <summary>
    /// Creates a new instance of a valid <see cref="Result"/>.
    /// </summary>
    public static Result Ok() => new(true, default);


    /// <summary>
    /// Creates a new instance of a failed <see cref="Result{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Result{T}.Value"/> of the created result.</typeparam>
    /// <param name="message">The message that describes the reason of this failed result.</param>
    public static Result<T> Fail<T>(string message) => new(false, default, message);

    /// <summary>
    /// Creates a new instance of a failed <see cref="Result"/>.
    /// </summary>
    public static Result Fail(string message) => new(false, message);
}
