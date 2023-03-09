using System.Text;

namespace Kay;

public class Interpreter : Dictionary<string, Entry>
{
    public Interpreter()
    {
        this["+"] = new Entry(Operations.Add)
        {
            Effect = "M I -> N",
            Help = new[]
            {
                "Numeric N is the result of adding numeric I to numeric N.",
            },
            Category = Category.Operator,
        };

        this["-"] = new Entry(Operations.Sub)
        {
            Effect = "M I -> N",
            Help = new[]
            {
                "Numeric N is the result of subtracting numeric I from numeric N.",
            },
            Category = Category.Operator,
        };

        this["*"] = new Entry(Operations.Mul)
        {
            Effect = "I J -> K",
            Help = new[]
            {
                "Numeric K is the result of multiplying numeric I by numeric J.",
            },
            Category = Category.Operator,
        };

        this["/"] = new Entry(Operations.Div)
        {
            Effect = "I J -> K",
            Help = new[]
            {
                "Numeric K is the result of dividing numeric I by numeric J.",
            },
            Category = Category.Operator,
        };

        this["<"] = new Entry(Operations.Lt)
        {
            Effect = "X Y -> B",
            Help = new[]
            {
                "Either both X and Y are numeric or both are strings or symbols.",
                "B is `true' when X < Y otherwise `false'.",
            },
            Category = Category.Predicate,
        };

        this[">"] = new Entry(Operations.Gt)
        {
            Effect = "X Y -> B",
            Help = new[]
            {
                "Either both X and Y are numeric or both are strings or symbols.",
                "B is `true' when X > Y otherwise `false'.",
            },
            Category = Category.Predicate,
        };

        this["<="] = new Entry(Operations.Lte)
        {
            Effect = "X Y -> B",
            Help = new[]
            {
                "Either both X and Y are numeric or both are strings or symbols.",
                "B is `true' when X <= Y otherwise `false'.",
            },
            Category = Category.Predicate,
        };

        this[">="] = new Entry(Operations.Gte)
        {
            Effect = "X Y -> B",
            Help = new[]
            {
                "Either both X and Y are numeric or both are strings or symbols.",
                "B is `true' when X >= Y otherwise `false'.",
            },
            Category = Category.Predicate,
        };

        this["="] = new Entry(Operations.Eq)
        {
            Effect = "X Y -> B",
            Help = new[]
            {
                "Either both X and Y are numeric or both are strings or symbols.",
                "B is `true' when X = Y otherwise `false'.",
            },
            Category = Category.Predicate,
        };

        this["!="] = new Entry(Operations.Neq)
        {
            Effect = "X Y -> B",
            Help = new[]
            {
                "Either both X and Y are numeric or both are strings or symbols.",
                "B is `true' when X != Y otherwise `false'.",
            },
            Category = Category.Predicate,
        };

        this["id"] = new Entry(_ => { })
        {
            Effect = "->",
            Help = new[]
            {
                "Identify function, does nothing.",
                "Any program of the form `P id Q' is equivalent to just `P Q'."
            },
            Category = Category.Operator,
        };

        this["i"] = new Entry(Operations._I)
        {
            Effect = "[P] -> ...",
            Help = new[]
            {
                "Executes P. So, [P] i == P.",
            },
            Category = Category.Combinator,            
        };

        this["x"] = new Entry(Operations._X)
        {
            Effect = "[P] -> [P] ...",
            Help = new[]
            {
                "Executes P without popping [P]. So, [P] x == [P] P.",
            },
            Category = Category.Combinator,            
        };

        this["pop"] = new Entry(Operations.Pop)
        {
            Effect = "X ->",
            Help = new[]
            {
                "Removes X from the top of the stack.",
            },
            Category = Category.Operator,
        };

        this["swap"] = new Entry(Operations.Swap)
        {
            Effect = "X Y -> Y X",
            Help = new[]
            {
                "Interchanges X and Y on top of the stack.",
            },
            Category = Category.Operator,
        };

        this["dip"] = new Entry(Operations.Dip)
        {
            Effect = "X [P] -> ... X",
            Help = new[]
            {
                "Saves X, executes P, pushes X back.",
            },
            Category = Category.Combinator,
        };

        this["cons"] = new Entry(Operations.Cons)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Operator,
        };

        this["dup"] = new Entry(Operations.Dup)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Operator,
        };

        this["clear"] = new Entry(Operations.Clear)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Misc,
        };

        this["trace"] = new Entry(Operations.Trace)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Misc,
        };

        this["branch"] = new Entry(Operations.Branch)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Combinator,
        };

        this["choice"] = new Entry(Operations.Choice)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Combinator,
        };

        this["first"] = new Entry(Operations.First)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Operator,
        };

        this["rest"] = new Entry(Operations.Rest)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Operator,
        };

        this["concat"] = new Entry(Operations.Concat)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Operator,
        };

        this["swaack"] = new Entry(Operations.Swaack)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Operator,
        };

        this["infra"] = new Entry(Operations.Infra)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Combinator,
        };

        this["map"] = new Entry(Operations.Map)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Combinator,
        };

        this["ifte"] = new Entry(Operations.Ifte)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Combinator,
        };

        this["unit"] = new Entry(Operations.Unit)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Operator,
        };

        this["intern"] = new Entry(Operations.Intern)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Operator,
        };

        this["name"] = new Entry(Operations.Name)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Operator,
        };

        this["stack"] = new Entry(Operations.Stack)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Operator,
        };

        this["unstack"] = new Entry(Operations.Unstack)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Operator,
        };

        this["type"] = new Entry(Operations.Type)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Operator,
        };

        this["char"] = new Entry(Operations.Char)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Predicate,
        };

        this["integer"] = new Entry(Operations.Integer)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Predicate,
        };

        this["float"] = new Entry(Operations.Float)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Predicate,
        };

        this["logical"] = new Entry(Operations.Logical)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
            Category = Category.Predicate,
        };

        this["primrec"] = new Entry(Operations.Primrec)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
        };

        this["time"] = new Entry(Operations.Time)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
        };

        this["body"] = new Entry(Operations.Body)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
        };

        this["user"] = new Entry(Operations.User)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
        };

        this["times"] = new Entry(Operations.Times)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
        };

        this["reverse"] = new Entry(Operations.Reverse)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
        };

        this["help"] = new Entry(Operations.Help)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
        };

        this["helpdetail"] = new Entry(Operations.Helpdetail)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
        };

        // Experimental
        this["def"] = new Entry(Operations.Def)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
        };

        // Experimental
        this["undef"] = new Entry(Operations.Undef)
        {
            Effect = "",
            Help = new[]
            {
                "",
            },
        };
    }

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
