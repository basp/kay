namespace Kay;

[AttributeUsage(AttributeTargets.Method)]
public class BuiltinAttribute : Attribute
{
    public BuiltinAttribute(string name, string effect, params string[] help)
    {
        this.Name = name;
        this.Effect = effect;
        this.Help = help;
    }

    public string Name { get; set; } = string.Empty;

    public string Effect { get; set; } = string.Empty;

    public string[] Help { get; set; }
}