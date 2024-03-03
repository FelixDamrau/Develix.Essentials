namespace Develix.Essentials.Core.Tests;

public class GraphBuilderTests
{
    [Fact]
    public void CreateEmptyGraph()
    {
        var graph = GraphBuilder<int>
            .Create()
            .Complete();

        graph.Vertices.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(GetSingleVertexValues))]
    public void CreateTrivialGraph<T>(T vertexValue)
        where T : IEquatable<T>
    {
        var graph = GraphBuilder<T>
            .Create()
            .AddVertex(vertexValue)
            .Complete();

        graph.Vertices.Should().ContainSingle();
        var singleVertex = graph.Vertices.Single();
        singleVertex.Value.Should().Be(vertexValue);
        singleVertex.Successors.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(GetSingleVertexValues))]
    public void AddEqualVertices<T>(T vertexValue)
        where T : IEquatable<T>
    {
        var graph = GraphBuilder<T>
            .Create()
            .AddVertex(vertexValue)
            .AddVertex(vertexValue)
            .Complete();

        graph.Vertices.Should().HaveCount(1);
        var vertex = graph.Vertices.Single();
        vertex.Value.Should().Be(vertexValue);
    }

    [Fact]
    public void AddEqualVerticesDifferentInstances()
    {
        var vertexValue = new Graph.EquatableTestClass(Guid.NewGuid());
        var sameVertexValue = new Graph.EquatableTestClass(vertexValue.Id);
        var graph = GraphBuilder<Graph.EquatableTestClass>
            .Create()
            .AddVertex(vertexValue)
            .AddVertex(sameVertexValue)
            .Complete();

        graph.Vertices.Should().HaveCount(1);
        var vertex = graph.Vertices.Single();
        vertex.Value.Should().Be(vertexValue);
    }

    [Theory]
    [MemberData(nameof(GetDoubleVerticesValues))]
    public void CreateDirectedGraph1<T>(T vertex1Value, T vertex2Value)
        where T : IEquatable<T>
    {
        var graph = GraphBuilder<T>
            .Create()
            .AddVertex(vertex1Value)
            .WithSuccessor(vertex2Value)
            .AddVertex(vertex2Value)
            .Complete();

        graph.Vertices.Should().HaveCount(2);
        var vertex1 = graph.Vertices.Single(v => v.Value.Equals(vertex1Value));
        vertex1.Successors.Should().ContainSingle();
        var vertex2 = graph.Vertices.Single(v => v.Value.Equals(vertex2Value));
        var vertex1Successor = vertex1.Successors.Single();
        vertex1Successor.Should().BeSameAs(vertex2);
    }

    [Theory]
    [MemberData(nameof(GetTripleVerticesValues))]
    public void CreateDirectedGraph2<T>(T vertex1Value, T vertex2Value, T vertex3Value)
        where T : IEquatable<T>
    {
        var graph = GraphBuilder<T>
            .Create()
            .AddVertex(vertex1Value)
            .WithSuccessors([vertex2Value, vertex3Value])
            .AddVertex(vertex2Value)
            .AddVertex(vertex3Value)
            .WithSuccessor(vertex1Value)
            .Complete();

        graph.Vertices.Should().HaveCount(3);
        var vertex1 = graph.Vertices.Single(v => v.Value.Equals(vertex1Value));
        vertex1.Successors.Should().HaveCount(2);
        var vertex2 = graph.Vertices.Single(v => v.Value.Equals(vertex2Value));
        var vertex1Successor1 = vertex1.Successors.Single(v => v.Value.Equals(vertex2Value));
        vertex1Successor1.Should().BeSameAs(vertex2);
        vertex2.Successors.Should().BeEmpty();
        var vertex3 = graph.Vertices.Single(v => v.Value.Equals(vertex3Value));
        var vertex1Successor2 = vertex1.Successors.Single(v => v.Value.Equals(vertex3Value));
        vertex1Successor2.Should().BeSameAs(vertex3);
        vertex3.Successors.Should().HaveCount(1);
        var vertex3Successor = vertex3.Successors.Single();
        vertex3Successor.Should().BeSameAs(vertex1);
    }

    public static TheoryData<object> GetSingleVertexValues()
    {
        return new TheoryData<object>()
        {
            42,
            36.7,
            "Banana",
            DateTime.Now,
            true,
            new Graph.EquatableTestClass(Guid.NewGuid())
        };
    }

    public static TheoryData<object, object> GetDoubleVerticesValues()
    {
        return new TheoryData<object, object>()
        {
            { 42, 69 },
            { 36.7, 9000.1 },
            { "Banana", "Rocket" },
            { DateTime.Now, DateTime.MinValue },
            { true, false },
            { new Graph.EquatableTestClass(Guid.NewGuid()), new Graph.EquatableTestClass(Guid.NewGuid()) },
        };
    }

    public static TheoryData<object, object, object> GetTripleVerticesValues()
    {
        return new TheoryData<object, object, object>()
        {
            { 42, 69, 367 },
            { 36.7, 9000.1, 12.0 },
            { "Banana", "Rocket", "Eggplant" },
            { DateTime.Now, DateTime.MinValue, DateTime.UnixEpoch },
            { new Graph.EquatableTestClass(Guid.NewGuid()), new Graph.EquatableTestClass(Guid.NewGuid()), new Graph.EquatableTestClass(Guid.NewGuid()) },
        };
    }
}
