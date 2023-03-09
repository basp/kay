namespace Kay;

public sealed class Category
{
    private Category(string name)
    {
        this.Name = name;
    }

    public string Name { get; }

    public static readonly Category Unknown = new Category("unknown");

    public static readonly Category Operand = new Category("operand");

    public static readonly Category Operator = new Category("operator");

    public static readonly Category Predicate = new Category("predicate");

    public static readonly Category Combinator = new Category("combinator");

    public static readonly Category Misc = new Category("misc");
}
