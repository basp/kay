namespace Kay;

using System.Diagnostics.CodeAnalysis;

public abstract partial class Node
{
    /// <summary>
    /// Represents a list node in the interpreter.
    /// </summary>
    public class List : Node, IAggregate
    {
        private readonly IList<INode> elements;

        public List(params INode[] elements)
        {
            this.elements = elements.ToList();
        }

        public List([NotNull] IEnumerable<INode> elements)
            : this(elements.ToArray())
        {
        }

        public override bool IsQuote => this.elements.Any(x => x.Op == Operand.Symbol);

        public IEnumerable<INode> Elements => this.elements;

        public override Operand Op => Operand.List;

        public override bool IsAggregate => true;

        public int Size => this.elements.Count;

        public override INode Clone()
        {
            var xs = this.elements.Select(x => x.Clone());
            return new Node.List(xs);
        }

        public override string ToString()
        {
            var xs = this.elements
                .Select(x => x.ToString())
                .ToArray();
            return $"[{string.Join(' ', xs)}]";
        }

        public override string ToRepresentation()
        {
            var xs = this.elements
                .Select(x => x.ToRepresentation())
                .ToArray();
            return $"[{string.Join(' ', xs)}]";
        }

        public override bool Equals(object? obj)
        {
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = obj as List;
            if (other == null)
            {
                return false;
            }

            return this.elements == other.elements;
        }

        public override int GetHashCode() =>
            HashCode.Combine(HashTags.List, this.elements.GetHashCode());

        public INode Cons(INode node) =>
            new List(new[] { node }.Concat(this.elements));

        public INode Concat(INode node) =>
            node switch
            {
                Node.List y => new List(this.elements.Concat(y.elements)),
                _ => throw new NotSupportedException(),
            };

        public INode First() => this.elements.First();

        public IAggregate Rest() => new Node.List(this.elements.Skip(1));

        public INode At(int index) => this.elements[index];
    }
}
