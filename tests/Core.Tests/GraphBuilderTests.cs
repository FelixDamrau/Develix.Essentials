﻿namespace Develix.Essentials.Core.Tests;

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
    public void CreateDirectedGraph<T>(T vertex1Value, T vertex2Value)
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

    public static IEnumerable<object[]> GetSingleVertexValues()
    {
        yield return new object[] { 42 };
        yield return new object[] { 36.7 };
        yield return new object[] { "Banana" };
        yield return new object[] { DateTime.Now };
        yield return new object[] { true };
        yield return new object[] { new Graph.EquatableTestClass(Guid.NewGuid()) };
    }

    public static IEnumerable<object[]> GetDoubleVerticesValues()
    {
        yield return new object[] { 42, 69 };
        yield return new object[] { 36.7, 9000.1 };
        yield return new object[] { "Banana", "Rocket" };
        yield return new object[] { DateTime.Now, DateTime.MinValue };
        yield return new object[] { true, false };
        yield return new object[] { new Graph.EquatableTestClass(Guid.NewGuid()), new Graph.EquatableTestClass(Guid.NewGuid()) };
    }
}
