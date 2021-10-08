using System.Diagnostics.CodeAnalysis;

namespace Develix.Essentials.Core;

public class Result<T>
{
    private readonly T? value;

    [MemberNotNullWhen(true, nameof(value))]
    [MemberNotNullWhen(true, nameof(ValueOrDefault))]
    [MemberNotNullWhen(false, nameof(Message))]
    public bool Valid { get; }
    
    public T Value
    {
        get
        {
            if (!Valid)
                throw new InvalidOperationException("Cannot get the value of an invalid result!");
            return value;
        }
    }

    public T? ValueOrDefault => value;

    public string? Message { get; }

    internal Result(bool valid, T? value, string? message)
    {
        Valid = valid;
        this.value = value;
        Message = message;
    }
}
