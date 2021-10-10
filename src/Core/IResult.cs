using System.Diagnostics.CodeAnalysis;

namespace Develix.Essentials.Core;
/// <summary>
/// Represents the state of an evaluation.
/// </summary>
public interface IResult
{
    /// <summary>
    /// The message that describes the state of this <see cref="IResult"/>, for instance, an error message.
    /// </summary>
    string? Message { get; }

    /// <summary>
    /// Specifies whether or not this <see cref="Result{T}"/> represents a successful evaluation.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Message))]
    bool Valid { get; }
}
