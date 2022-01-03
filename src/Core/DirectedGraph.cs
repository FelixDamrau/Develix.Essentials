namespace Develix.Essentials.Core;

public interface IDirectedGraph<TVertex>
{
    IReadOnlyList<IVertex<TVertex>> Vertices { get; }
}

internal class DirectedGraph<TVertex> : IDirectedGraph<TVertex>
    where TVertex : IEquatable<TVertex>
{
    public List<Vertex<TVertex>> Vertices { get; } = new();

    IReadOnlyList<IVertex<TVertex>> IDirectedGraph<TVertex>.Vertices => Vertices;
}

public interface IVertex<T>
{
    IReadOnlyList<IVertex<T>> Successors { get; }
    T Value { get; init; }
}

internal class Vertex<T> : IVertex<T>
    where T : IEquatable<T>
{
    public T Value { get; init; }
    public List<Vertex<T>> Successors { get; } = new();

    IReadOnlyList<IVertex<T>> IVertex<T>.Successors => Successors;
}
