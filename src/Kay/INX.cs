namespace Kay;

public class INX : Interpreter
{
    /// <summary>
    /// Provides an experimental interpreter implementation.
    /// </summary>
    public INX()
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

        this["quit"] = new Entry(Operations.Quit)
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
}