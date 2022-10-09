namespace Develix.Essentials.Core;

/// <summary>
/// A directed graph with vertices of type <typeparamref name="TVertex"/>.
/// </summary>
public interface IDirectedGraph<TVertex>
{
    /// <summary>
    /// A collection of all vertices of this directed graph.
    /// </summary>
    IReadOnlyList<IVertex<TVertex>> Vertices { get; }
}

internal class DirectedGraph<TVertex> : IDirectedGraph<TVertex>
    where TVertex : IEquatable<TVertex>
{
    public List<Vertex<TVertex>> Vertices { get; } = new();

    IReadOnlyList<IVertex<TVertex>> IDirectedGraph<TVertex>.Vertices => Vertices;
}

/// <summary>
/// A vertex of type <typeparamref name="T"/> of an <see cref="IDirectedGraph{TVertex}"/>.
/// </summary>
public interface IVertex<T>
{
    /// <summary>
    /// All successors of this vertex.
    /// </summary>
    IReadOnlyList<IVertex<T>> Successors { get; }

    /// <summary>
    /// The value of this vertex.
    /// </summary>
    T Value { get; }
}

internal class Vertex<T> : IVertex<T>
    where T : IEquatable<T>
{
    public Vertex(T value)
    {
        Value = value;
    }

    public T Value { get; }
    public List<Vertex<T>> Successors { get; } = new();

    IReadOnlyList<IVertex<T>> IVertex<T>.Successors => Successors;
}
