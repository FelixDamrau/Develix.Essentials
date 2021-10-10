using System.Diagnostics.CodeAnalysis;

namespace Develix.Essentials.Core;

/// <summary>
/// This class represents a successful or failed evaluation of a function with codomain <typeparamref name="T"/>,
/// for instance, a method with the return type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Result<T>
{
    private readonly T? value;

    /// <summary>
    /// Specifies whether or not this <see cref="Result{T}"/> represents a successful evaluation.
    /// </summary>
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

    /// <summary>
    /// The value of a successfully evaluated <see cref="Result{T}"/> if the result is valid,
    /// otherwise the default value of <typeparamref name="T"/>.
    /// </summary>
    public T? ValueOrDefault => value;

    /// <summary>
    /// The message that describes the state of this <see cref="Result{T}"/>, for instance, an error message.
    /// </summary>
    public string? Message { get; }

    internal Result(bool valid, T? value, string? message)
    {
        Valid = valid;
        this.value = value;
        Message = message;
    }
}
