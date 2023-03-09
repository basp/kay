namespace Kay;

using System.Text;

/// <summary>
/// This class contains definitions for all kinds of operations
/// and combinators that can be used to bootstrap an interpreter
/// environment.
/// </summary>
public static class Operations
{
    private static readonly Node.List ifte =
        new Node.List(
            Node.Symbol.Get("infra"),
            Node.Symbol.Get("first"),
            Node.Symbol.Get("choice"),
            Node.Symbol.Get("i"));

    [Builtin("+", "X Y  ->  Z", "Z = X + Y")]
    public static void Add(Interpreter i) =>
        BinaryArithmetic(i, "+", (x, y) => x.Add(y));

    public static void Sub(Interpreter i) =>
        BinaryArithmetic(i, "-", (x, y) => x.Subtract(y));

    public static void Mul(Interpreter i) =>
        BinaryArithmetic(i, "*", (x, y) => x.Multiply(y));

    public static void Div(Interpreter i) =>
        BinaryArithmetic(i, "/", (x, y) => x.Divide(y));

    public static void Mod(Interpreter i) =>
        BinaryArithmetic(i, "%", (x, y) => x.Modulo(y));

    public static void Lt(Interpreter i) =>
        BinaryLogic(i, "<", (x, y) => x.CompareTo(y) < 0
            ? Node.Boolean.True
            : Node.Boolean.False);

    public static void Gt(Interpreter i) =>
        BinaryLogic(i, ">", (x, y) => x.CompareTo(y) > 0
            ? Node.Boolean.True
            : Node.Boolean.False);

    public static void Lte(Interpreter i) =>
        BinaryLogic(i, "<=", (x, y) => x.CompareTo(y) <= 0
            ? Node.Boolean.True
            : Node.Boolean.False);

    public static void Gte(Interpreter i) =>
        BinaryLogic(i, ">=", (x, y) => x.CompareTo(y) >= 0
            ? Node.Boolean.True
            : Node.Boolean.False);

    public static void Eq(Interpreter i) =>
        BinaryLogic(i, "=", (x, y) => x.CompareTo(y) == 0
            ? Node.Boolean.True
            : Node.Boolean.False);

    public static void Neq(Interpreter i) =>
        BinaryLogic(i, "!=", (x, y) => x.CompareTo(y) != 0
            ? Node.Boolean.True
            : Node.Boolean.False);

    public static void Body(Interpreter i)
    {
        Validators.BodyValidator.Validate(i.Stack);
        string msg;
        var s = i.Pop<Node.Symbol>();
        if (!i.TryGetValue(s.Name, out var entry))
        {
            msg = $"undefined symbol `{s.Name}'";
            throw new RuntimeException(msg);
        }
        if (!entry.IsUserDefined)
        {
            msg = $"`{s.Name}' is a built-in";
            throw new RuntimeException(msg);
        }
        i.Stack.Push(new Node.List(entry.Body));
    }

    public static void Null(Interpreter i)
    {
        new Validator("null")
            .OneArgument()
            .Validate(i.Stack);
        var x = i.Pop<INode>();
        i.Push(Node.Boolean.Get(Node.IsNull(x)));
    }

    public static void Name(Interpreter i)
    {
        Validators.NameValidator.Validate(i.Stack);
        var x = i.Pop<INode>();
        var z = x.Op == Operand.Symbol
            ? new Node.String(((Node.Symbol)x).Name)
            : new Node.String(x.Op.ToString());
        i.Push(z);
    }

    public static void Unit(Interpreter i)
    {
        Validators.UnitValidator.Validate(i.Stack);
        var x = i.Pop<INode>();
        i.Push(new Node.List(x));
    }

    public static void Intern(Interpreter i)
    {
        Validators.InternValidator.Validate(i.Stack);
        var x = i.Pop<Node.String>();
        i.Push(Node.Symbol.Get(x.Value));
    }

    public static void Swap(Interpreter i)
    {
        Validators.SwapValidator.Validate(i.Stack);
        var y = i.Pop<INode>();
        var x = i.Pop<INode>();
        i.Push(y);
        i.Push(x);
    }

    public static void Cons(Interpreter i)
    {
        Validators.ConsValidator.Validate(i.Stack);
        var a = i.Pop<Node.List>();
        var x = i.Pop<INode>();
        i.Push(a.Cons(x));
    }

    public static void Branch(Interpreter i)
    {
        Validators.BranchValidator.Validate(i.Stack);
        var @else = i.Pop<Node.List>();
        var @then = i.Pop<Node.List>();
        var cond = i.Pop<INode>();
        var cons = Node.IsTruthy(cond) ? @then : @else;
        i.Queue.InsertFirst(cons!);
    }

    public static void Choice(Interpreter i)
    {
        Validators.ChoiceValidator.Validate(i.Stack);
        var cond = i.Pop<INode>();
        var @then = i.Pop<INode>();
        var @else = i.Pop<INode>();
        var cons = Node.IsTruthy(cond) ? @then : @else;
        i.Push(cons);
    }

    public static void Reverse(Interpreter i)
    {
        new Validator("reverse")
            .OneArgument()
            .ListOnTop()
            .Validate(i.Stack);
        var xs = i.Pop<Node.List>();
        i.Push(new Node.List(xs.Elements.Reverse()));
    }

    public static void Ifte(Interpreter i)
    {
        Validators.IfteValidator.Validate(i.Stack);

        i.Queue.InsertFirst(ifte);

        var @else = i.Pop<Node.List>();
        var @then = i.Pop<Node.List>();
        var cond = i.Pop<Node.List>();
        var saved = new Node.List(i.Stack);

        i.Stack.Push(@else);
        i.Stack.Push(@then);
        i.Stack.Push(saved);
        i.Stack.Push(cond);
    }

    public static void Dip(Interpreter i)
    {
        Validators.DipValidator.Validate(i.Stack);
        var quote = i.Pop<Node.List>();
        var x = i.Pop<INode>();
        i.Queue.InsertFirst(new Node.List(x));
        i.Queue.InsertFirst(quote);
    }

    public static void Concat(Interpreter i)
    {
        Validators.ConcatValidator.Validate(i.Stack);
        var y = i.Pop<IAggregate>();
        var x = i.Pop<IAggregate>();
        i.Push(x.Concat(y));
    }

    public static void Pop(Interpreter i)
    {
        Validators.PopValidator.Validate(i.Stack);
        i.Pop<INode>();
    }

    public static void First(Interpreter i)
    {
        Validators.FirstValidator.Validate(i.Stack);
        var xs = i.Pop<IAggregate>();
        i.Push(xs.First());
    }

    public static void Rest(Interpreter i)
    {
        Validators.RestValidator.Validate(i.Stack);
        var xs = i.Pop<IAggregate>();
        i.Push(xs.Rest());
    }

    public static void Reset(Interpreter i)
    {
        i.Stack = new C5.ArrayList<INode>();
        i.Queue = new C5.ArrayList<Node.List>();

    }

    public static void Clear(Interpreter i)
    {
        i.Stack = new C5.ArrayList<INode>();
    }

    public static void Dup(Interpreter i)
    {
        Validators.DupValidator.Validate(i.Stack);
        var x = i.Peek<INode>();
        i.Queue.InsertFirst(new Node.List(x));
    }

    public static void _X(Interpreter i)
    {
        Validators._XValidator.Validate(i.Stack);
        var quote = i.Peek<Node.List>();
        i.Queue.InsertFirst(quote);
    }

    public static void _I(Interpreter i)
    {
        Validators._IValidator.Validate(i.Stack);
        var quote = i.Pop<Node.List>();
        i.Queue.InsertFirst(quote);
    }

    /// <summary>
    /// stack       :  .. X Y Z  ->  .. X Y Z [Z Y X ..]
    /// Pushes the stack as a list.
    /// </summary>
    public static void Stack(Interpreter i)
    {
        i.Push(new Node.List(i.Stack.Reverse<INode>()));
    }

    /// <summary>
    /// unstack     :  [X Y ..]  ->  ..Y X
    /// The list [X Y ..] becomes the new stack.    
    /// </summary>
    public static void Unstack(Interpreter i)
    {
        Validators.UnstackValidator.Validate(i.Stack);
        var x = i.Pop<Node.List>();
        i.Stack.Clear();
        i.Stack.AddAll(x.Elements);
    }

    public static void Swaack(Interpreter i)
    {
        Validators.SwaackValidator.Validate(i.Stack);
        var @new = i.Pop<Node.List>();
        i.Stack.Reverse();
        var @old = new Node.List(i.Stack);
        i.Stack = new C5.ArrayList<INode>();
        i.Stack.AddAll(@new.Elements.Reverse());
        i.Push(@old);
    }

    /// <summary>
    /// infra      :  L1 [P]  ->  L2
    /// </summary>
    /// <remarks>
    /// Using list L1 as a stack, executes P and returns a new list L2.
    /// The first element of L1 is used as the top of the stack,
    /// and after execution of P the top of the stack becomes first 
    /// element of L2.
    /// </remarks>
    public static void Infra(Interpreter i)
    {
        Validators.InfraValidator.Validate(i.Stack);
        var q = i.Pop<Node.List>();
        var a = i.Pop<IAggregate>();
        i.Stack.Reverse();
        i.Queue.InsertFirst(new Node.List(Node.Symbol.Get("swaack")));
        i.Queue.InsertFirst(new Node.List(new Node.List(i.Stack)));
        i.Queue.InsertFirst(q);
        i.Stack = new C5.ArrayList<INode>();
        i.Stack.AddAll(a.Elements);
    }

    public static void Map(Interpreter i)
    {
        Validators.MapValidator.Validate(i.Stack);
        var q = i.Pop<Node.List>();
        var a = i.Pop<IAggregate>();
        if (!a.Elements.Any())
        {
            i.Stack.Push(a);
            return;
        }

        var batch = new C5.ArrayList<INode>();
        foreach (var x in a.Elements)
        {
            batch.InsertFirst(Node.Symbol.Get("first"));
            batch.InsertFirst(Node.Symbol.Get("infra"));
            batch.InsertFirst(q);
            batch.InsertFirst(new Node.List(x));
        }

        i.Push(new Node.List());
        i.Push(new Node.List(batch));
        i.Queue.InsertFirst(new Node.List(Node.Symbol.Get("infra")));
    }

    public static void Primrec(Interpreter i)
    {
        Validators.PrimrecValidator.Validate(i.Stack);

        var recur = i.Pop<Node.List>();
        var @base = i.Pop<Node.List>();
        var x = i.Pop<INode>();

        INode curr = x switch
        {
            IOrdinal ord => ord,
            IAggregate agg => !Node.IsEmpty(agg) ? agg.First() : agg,
            _ => throw new InvalidOperationException(),
        };

        INode next = x switch
        {
            IOrdinal ord => ord.Pred(),
            IAggregate agg => !Node.IsEmpty(agg) ? agg.Rest() : agg,
            _ => throw new InvalidOperationException(),
        };

        if (Node.IsNull(curr))
        {
            i.Queue.InsertFirst(@base);
        }
        else
        {
            i.Queue.InsertFirst(recur);
            i.Queue.InsertFirst(new Node.List(Node.Symbol.Get("primrec")));
            i.Stack.Push(curr);
            i.Stack.Push(next);
            i.Stack.Push(@base);
            i.Stack.Push(recur);
        }
    }

    public static void Undef(Interpreter i)
    {
        Validators.UndefValidator.Validate(i.Stack);

        var v = i.Pop<INode>();

        Node.Symbol sym = v switch
        {
            Node.Symbol x => x,
            Node.String s => Node.Symbol.Get(s.Value),
            _ => throw new InvalidOperationException(),
        };

        if (i.ContainsKey(sym.Name))
        {
            if (i[sym.Name].IsUserDefined)
            {
                i.Remove(sym.Name);
            }
            else
            {
                var msg = $"`{sym.Name}' is a built-in definition";
                throw new RuntimeException(msg);
            }
        }
    }

    public static void Type(Interpreter i)
    {
        Validators.TypeValidator.Validate(i.Stack);
        var x = i.Pop<INode>();
        i.Push(Node.Integer.Get((int)x.Op));
    }

    /// <summary>
    /// integer      :  X  ->  B
    /// Tests whether X is an integer.
    /// </summary>
    public static void Integer(Interpreter i) =>
        TypeCheck(i, "integer", Operand.Integer);

    /// <summary>
    /// char         :  X  ->  B
    /// Tests whether X is a character.
    /// </summary>
    public static void Char(Interpreter i) =>
        TypeCheck(i, "char", Operand.Char);


    /// <summary>
    /// logical      :  X  ->  B
    /// Tests whether X is a logical.
    /// </summary>
    public static void Logical(Interpreter i) =>
        TypeCheck(i, "logical", Operand.Boolean);

    public static void String(Interpreter i) =>
        TypeCheck(i, "string", Operand.String);

    public static void Float(Interpreter i) =>
        TypeCheck(i, "float", Operand.Float);

    public static void Set(Interpreter i) =>
        TypeCheck(i, "set", Operand.Set);

    public static void List(Interpreter i) =>
        TypeCheck(i, "list", Operand.List);

    public static void Leaf(Interpreter i) =>
        TypeCheck(i, "leaf", op => op != Operand.List);

    /// <summary>
    /// times      :  N [P]  ->  ...
    /// N times executes P.
    /// </summary>
    public static void Times(Interpreter i)
    {
        Validators.TimesValidator.Validate(i.Stack);
        var p = i.Pop<Node.List>();
        var n = i.Pop<Node.Integer>();
        var batch = Enumerable.Range(0, n.Value).Select(_ => p);
        i.Queue.InsertAll(0, batch);
    }

    /// <summary>
    /// user      :  X  ->  B
    /// Tests whether X is a user-defined symbol.
    /// </summary>
    public static void User(Interpreter i)
    {
        Validators.UserValidator.Validate(i.Stack);
        var x = i.Pop<Node.Symbol>();
        if (i.TryGetValue(x.Name, out var entry))
        {
            i.Push(Node.Boolean.Get(entry.IsUserDefined));
        }
        else
        {
            i.Push(Node.Boolean.False);
        }
    }

    /// <summary>
    /// rollup      :  X Y Z  ->  Z X Y
    /// </summary>
    public static void Rollup(Interpreter i)
    {
        new Validator("rollup")
            .ThreeArguments()
            .Validate(i.Stack);
        var z = i.Pop<INode>();
        var y = i.Pop<INode>();
        var x = i.Pop<INode>();
        i.Push(z);
        i.Push(x);
        i.Push(y);
    }

    /// <summary>
    /// rolldown    :  X Y Z  ->  Y Z X
    /// </summary>
    public static void Rolldown(Interpreter i)
    {
        new Validator("rolldown")
            .ThreeArguments()
            .Validate(i.Stack);
        var z = i.Pop<INode>();
        var y = i.Pop<INode>();
        var x = i.Pop<INode>();
        i.Push(y);
        i.Push(z);
        i.Push(x);
    }

    public static void Time(Interpreter i)
    {
        new Validator("time")
            .OneArgument()
            .OneQuote()
            .Validate(i.Stack);
        var saved = i.Queue;
        var then = DateTime.Now;
        var p = i.Pop<Node.List>();
        i.Execute(p.Elements);
        var now = DateTime.Now;
        var dt = (now - then).TotalMilliseconds;
        Console.WriteLine($"<{(int)dt} ms>");
    }

    /// <summary>
    /// ... A [Q] def  ->  ...
    ///
    /// The value <c>A</c> should be a symbol. Taking the symbolic name of 
    /// <c>A</c> this will assign <c>[Q]</c> as the body of <c>A</c> adding it
    /// to the entries in the interpreter environment.
    /// </summary>
    /// <remarks>
    /// This is equivelent to writing <c>A == Q.</c> but does not require the
    /// parser to be in cycle mode. In essence this allows you to have global
    /// variables.
    /// </remarks>
    public static void Def(Interpreter i)
    {
        new Validator("def")
            .TwoArguments()
            .OneQuote()
            .SymbolAsSecond()
            .Validate(i.Stack);
        var q = i.Pop<Node.List>();
        var n = i.Pop<Node.Symbol>();
        i.AddDefinition(n.Name, q.Elements);
    }

    public static void Trace(Interpreter i)
    {
        Validators.TraceValidator.Validate(i.Stack);

        Tracer.MaxColumnWidth = 40;

        var history = new Tracer();
        var saved = i.Queue;

        i.Queue = new C5.ArrayList<Node.List>();
        i.Queue.InsertFirst(i.Pop<Node.List>());

        history.Record(i);

        while (i.TryDequeue(out var node))
        {
            if (history.Count > 1000)
            {
                break;
            }

            if (node!.Op == Operand.Symbol)
            {
                var symbol = (Node.Symbol)node;
                if (!i.TryGetValue(symbol.Name, out var entry))
                {
                    var msg = $"Unknown symbol: '{symbol.Name}'";
                    throw new RuntimeException(msg);
                }

                if (entry.IsUserDefined)
                {
                    i.Queue.InsertFirst(new Node.List(entry.Body));
                }
                else
                {
                    entry.Action(i);
                }
            }
            else
            {
                i.Push(node);
            }

            history.Record(i);
        }

        i.Queue = saved;

        Console.WriteLine(history.ToString());
    }

    public static void Help(Interpreter i)
    {
        var defs = i
            .Where(x => x.Value.IsUserDefined)
            .Select(x => new
            {
                Id = x.Key,
                Body = string.Join(
                    " ",
                    x.Value.Body.Select(y => y.ToRepresentation())),
            });

        var builtins = i
            .Where(x => !x.Value.IsUserDefined)
            .Select(x => new
            {
                Id = x.Key,
                Effect = x.Value.Effect,
                Help = x.Value.Help,
                Category = x.Value.Category,
            });

        var buf = new StringBuilder();
        int padding;

        if (defs.Any())
        {
            padding = defs.Max(x => x.Id.Length);
            foreach (var t in defs.OrderBy(x => x.Id))
            {
                buf.Append(t.Id.PadLeft(padding));
                buf.Append(" == ");
                buf.AppendLine(t.Body);
            }
        }

        if (buf.Length != 0)
        {
            // Only add blank line if we have any definitions at all.
            buf.AppendLine();
        }

        if (builtins.Any())
        {
            padding = builtins.Max(x => x.Id.Length);
            var categories = builtins.GroupBy(x => x.Category);
            foreach (var c in categories)
            {
                buf.AppendLine(c.Key.Name);
                buf.AppendLine("".PadLeft(80, '-'));
                foreach(var t in c)
                {
                    buf.Append(t.Id.PadRight(padding));
                    buf.Append(" : ");
                    buf.AppendLine(t.Effect);
                }

                buf.AppendLine();
            }
        }

        Console.WriteLine(buf.ToString());
    }

    public static void Helpdetail(Interpreter i)
    {
        new Validator("helpdetail")
            .ListOnTop()
            .Validate(i.Stack);

        var ids = i.Pop<Node.List>();
        var buf = new StringBuilder();

        if (Node.IsEmpty(ids))
        {
            return;
        }

        var nodes = ids.Elements
            .Where(x => x.Op == Operand.Symbol)
            .Cast<Node.Symbol>()
            .Where(x => i.ContainsKey(x.Name))
            .ToList();

        var padding = nodes.Max(x => x.Name.Length);
        foreach (var node in nodes)
        {
            var entry = i[node.Name];
            buf.AppendLine(string.Concat(node.Name.PadRight(padding), " : ", entry.Effect));
            if (entry.Help.Length > 0)
            {
                foreach (var line in entry.Help)
                {
                    buf.AppendLine(line);
                }
            }

            if (nodes.Count > 1)
            {
                buf.AppendLine();
            }
        }

        Console.WriteLine(buf.ToString());
    }

    private static void BinaryLogic(
        Interpreter i,
        string name,
        Func<IOrdinal, IOrdinal, INode> cmp)
    {
        new Validator(name)
            .TwoArguments()
            .TwoOrdinalsOnTop()
            .Validate(i.Stack);
        var y = i.Pop<IOrdinal>();
        var x = i.Pop<IOrdinal>();
        i.Push(cmp(x, y));
    }

    private static void BinaryArithmetic(
        Interpreter i,
        string name,
        Func<IFloatable, IFloatable, IFloatable> op)
    {
        new Validator(name)
            .TwoArguments()
            .TwoFloatsOrIntegers()
            .Validate(i.Stack);
        var y = i.Pop<IFloatable>();
        var x = i.Pop<IFloatable>();
        i.Push(op(x, y));
    }

    private static void TypeCheck(
        Interpreter i,
        string typeName,
        Func<Operand, bool> pred)
    {
        new Validator(typeName)
            .OneArgument()
            .Validate(i.Stack);
        var x = i.Pop<INode>();
        var z = Node.Boolean.Get(pred(x.Op));
        i.Push(z);
    }

    private static void TypeCheck(
        Interpreter i,
        string typeName,
        Operand rand) => TypeCheck(i, typeName, op => op == rand);
}
