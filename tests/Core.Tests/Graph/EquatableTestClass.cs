namespace Develix.Essentials.Core.Tests.Graph
{
#pragma warning disable CA1067 // Override Object.Equals(object) when implementing IEquatable<T> - Reason: Test class uses the absolute minimum requirements
    public class EquatableTestClass : IEquatable<EquatableTestClass>
#pragma warning restore CA1067 // Override Object.Equals(object) when implementing IEquatable<T>
    {
        public Guid Id { get; }

        public EquatableTestClass(Guid id)
        {
            Id = id;
        }

        public bool Equals(EquatableTestClass? other)
        {
            return other?.Id == Id;
        }
    }
}
