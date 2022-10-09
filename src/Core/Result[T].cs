using System.Diagnostics.CodeAnalysis;

namespace Develix.Essentials.Core;

/// <summary>
/// This class represents a successful or failed evaluation of a function with codomain <typeparamref name="T"/>,
/// for instance, a method with the return type <typeparamref name="T"/>.
/// </summary>
public class Result<T> : IResult<T>
{
    private readonly T? value;

    /// <inheritdoc />
    [MemberNotNullWhen(true, nameof(value))]
    [MemberNotNullWhen(true, nameof(ValueOrDefault))]
    [MemberNotNullWhen(false, nameof(Message))]
    public bool Valid { get; }

    /// <summary>
    /// The value of a successfully evaluated <see cref="Result{T}"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if and only if this <see cref="Result{T}"/> is not valid.</exception>
    public T Value
    {
        get
        {
            if (!Valid)
                throw new InvalidOperationException("Cannot get the value of an invalid result!");
            return value;
        }
    }

    /// <inheritdoc />
    public T? ValueOrDefault => value;

    /// <inheritdoc />
    public string? Message { get; }

    internal Result(bool valid, T? value, string? message)
    {
        Valid = valid;
        this.value = value;
        Message = message;
    }
}
