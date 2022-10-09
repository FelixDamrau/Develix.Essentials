namespace Develix.Essentials.Core
{
    /// <summary>
    /// This is a covariant interface for <see cref="Result{T}"/>.
    /// </summary>
    public interface IResult<out T> : IResult
    {
        /// <summary>
        /// The value of a successfully evaluated <see cref="Result{T}"/>.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// The value of a successfully evaluated <see cref="Result{T}"/> if the result is valid,
        /// otherwise the default value of <typeparamref name="T"/>.
        /// </summary>
        T? ValueOrDefault { get; }
    }
}
