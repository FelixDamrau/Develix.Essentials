namespace Develix.Essentials.Core;

/// <summary>
/// This class is used to create instances of <see cref="DirectedGraph{TVertex}"/>.
/// </summary>
/// <typeparam name="TVertex">The type of the to be built graph.</typeparam>
public class GraphBuilder<TVertex> : IGraphBuilderPhase<TVertex>, IVertexAddedPhase<TVertex>
    where TVertex : IEquatable<TVertex>
{
    private readonly DirectedGraph<TVertex> graph = new();
    private Vertex<TVertex>? lastAddedVertex;

    /// <summary>
    /// Creates a new instance of a <see cref="GraphBuilder{TVertex}"/>
    /// </summary>
    public static IGraphBuilderPhase<TVertex> Create() => new GraphBuilder<TVertex>();

    IVertexAddedPhase<TVertex> IGraphBuilderPhase<TVertex>.AddVertex(TVertex vertexValue)
    {
        if (graph.Vertices.FirstOrDefault(v => v.Value.Equals(vertexValue)) is { } existingVertex)
        {
            lastAddedVertex = existingVertex;
        }
        else
        {
            var vertex = new Vertex<TVertex> { Value = vertexValue };
            graph.Vertices.Add(vertex);
            lastAddedVertex = vertex;
        }
        return this;
    }

    IDirectedGraph<TVertex> IGraphBuilderPhase<TVertex>.Complete() => graph;

    IVertexAddedPhase<TVertex> IVertexAddedPhase<TVertex>.WithSuccessor(TVertex vertexValue)
    {
        if (lastAddedVertex is null)
            throw new InvalidOperationException($"The graph builder is in an invalid state. (That's really bad)");

        if (graph.Vertices.SingleOrDefault(v => v.Value.Equals(vertexValue)) is { } existingVertex)
        {
            lastAddedVertex.Successors.Add(existingVertex);
        }
        else
        {
            var newVertex = new Vertex<TVertex> { Value = vertexValue };
            graph.Vertices.Add(newVertex);
            lastAddedVertex.Successors.Add(newVertex);
        }
        return this;
    }

    IGraphBuilderPhase<TVertex> IVertexAddedPhase<TVertex>.WithSuccessors(IEnumerable<TVertex> vertices)
    {
        foreach (var vertex in vertices)
        {
            ((IVertexAddedPhase<TVertex>)this).WithSuccessor(vertex);
        }
        lastAddedVertex = null;
        return this;
    }
}

/// <summary>
/// The phase of a <see cref="GraphBuilder{TVertex}"/> where once can add successors to a graph vertex.
/// </summary>
/// <typeparam name="TVertex">The type of the currently built graph.</typeparam>
public interface IVertexAddedPhase<TVertex> : IGraphBuilderPhase<TVertex>
{
    /// <summary>
    /// Add a successor to the last added graph vertex.
    /// </summary>
    /// <param name="vertex">The successor of the last added graph vertex.</param>
    /// <returns>The current <see cref="GraphBuilder{TVertex}"/> instance.</returns>
    public IVertexAddedPhase<TVertex> WithSuccessor(TVertex vertex);

    /// <summary>
    /// Add multiple successors to the last added graph vertex.
    /// </summary>
    /// <param name="vertices">The successors of the last added graph vertex.</param>
    /// <returns>The current <see cref="GraphBuilder{TVertex}"/> instance.</returns>
    public IGraphBuilderPhase<TVertex> WithSuccessors(IEnumerable<TVertex> vertices);
}

/// <summary>
/// The phase of a <see cref="GraphBuilder{TVertex}"/> where once can add vertices to a graph.
/// </summary>
/// <typeparam name="TVertex">The type of the currently built graph.</typeparam>
public interface IGraphBuilderPhase<TVertex>
{
    /// <summary>
    /// Adds a vertex to the graph.
    /// </summary>
    /// <param name="vertex">The vertex to be addded.</param>
    /// <returns>The current <see cref="GraphBuilder{TVertex}"/> instance.</returns>
    public IVertexAddedPhase<TVertex> AddVertex(TVertex vertex);

    /// <summary>
    /// Completes the graph builder process.
    /// </summary>
    /// <returns>The finished built graph of this <see cref="GraphBuilder{TVertex}"/>.</returns>
    public IDirectedGraph<TVertex> Complete();
}
