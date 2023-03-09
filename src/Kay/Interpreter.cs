using System.Text;

namespace Kay;

public abstract class Interpreter : Dictionary<string, Entry>
{
    /// <summary>
    /// Gets or sets the interpreter stack.
    /// </summary>
    public C5.ArrayList<INode> Stack { get; set; } =
        new C5.ArrayList<INode>(1024);

    /// <summary>
    /// Gets or sets the interpreter queue.
    /// </summary>
    public C5.ArrayList<Node.List> Queue { get; set; } =
        new C5.ArrayList<Node.List>(1024);

    /// <summary>
    /// Adds a new runtime definition to the interpreter environment.
    /// </summary>
    public void AddDefinition(string name, IEnumerable<INode> body)
    {
        var entry = new Entry(body);
        if (!this.TryAdd(name, entry))
        {
            string msg;
            if (this[name].IsUserDefined)
            {
                msg = string.Concat(
                    $"`{name}' is already defined as: ",
                    string.Join(
                        ' ',
                        this[name].Body.Select(x => x.ToRepresentation())));
                throw new RuntimeException(msg);
            }
            else
            {
                msg = $"`{name}' is a built-in definition";
                throw new RuntimeException(msg);
            }
        }
    }

    public void Redefine(string name, IEnumerable<INode> body)
    {
        this.Remove(name);
        AddDefinition(name, body);
    }

    /// <summary>
    /// Executes a term (a list of factors) as a new queue.
    /// </summary>
    /// <remarks>
    /// Note that this should only be called (once) every cycle from the 
    /// top-level since it will reset the current queue. It is not
    /// recommended to invoke this from inside an interpreter operation
    /// unless you save and restore the queue or intentionally want
    /// to destroy it.
    /// </remarks>
    public void Execute(IEnumerable<INode> factors)
    {
        this.Queue.Clear();
        this.Queue.InsertFirst(new Node.List(factors));
        while (this.TryDequeue(out var node))
        {
            if (node!.Op == Operand.Symbol)
            {
                var symbol = (Node.Symbol)node;
                if (!this.TryGetValue(symbol.Name, out var entry))
                {
                    var msg = $"Unknown symbol: '{symbol.Name}'";
                    throw new RuntimeException(msg);
                }

                if (entry.IsUserDefined)
                {
                    this.Queue.InsertFirst(new Node.List(entry.Body));
                }
                else
                {
                    entry.Action(this);
                }
            }
            else
            {
                this.Stack.Push(node);
            }
        }
    }

    /// <summary>
    /// Peek at the value on top of the stack.
    /// </summary>
    public T Peek<T>() where T : INode => (T)this.Stack.Last();

    /// <summary>
    /// Pops a node from the stack.
    /// </summary>
    public T Pop<T>() where T : INode => (T)this.Stack.Pop();

    /// <summary>
    /// Pushes a node onto the stack.
    /// </summary>
    public void Push(INode node) => this.Stack.Push(node);

    /// <summary>
    /// Tries to dequeue a value from the queue.
    /// </summary>
    /// <remarks>
    /// <p>
    /// Values on the queue are always assumed to be quoted. This means that 
    /// a list literal such as <c>[1 2 3]</c> will appear as <c>[[1 2 3]]</c> 
    /// and literals such as <c>true</c>, <c>1</c>, <c>"foo"</c> will appear 
    /// as <c>[true]</c>, <c>[1]</c> and <c>["foo"]</c> or (when they are 
    /// packed into a single quotation) as <c>[true 1 "foo"]</c>. The packed
    /// and unpacked forms are semantically equivalent to the interpreter.
    /// </p>
    /// </remarks>
    public bool TryDequeue(out INode? node) => TryDequeue(this.Queue, out node);

    private static bool TryDequeue(C5.ArrayList<Node.List> queue, out INode? node)
    {
        node = null;

        if (!queue.Any())
        {
            return false;
        }

        var quote = queue.RemoveFirst();
        node = quote.Elements.FirstOrDefault();
        if (node == null)
        {
            return false;
        }

        var rest = quote.Elements.Skip(1);
        if (rest.Any())
        {
            queue.InsertFirst(new Node.List(rest));
        }

        return true;
    }
}
