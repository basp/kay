namespace Kay;

/// <summary>
/// This class contains the validators for most of the 
/// built-ins defined in the <see cref="Operations"/> class.
/// </summary>
public static class Validators
{    
    public static readonly Validator UserValidator =
        new Validator("user")
            .OneArgument()
            .SymbolOnTop();

    public static readonly Validator TimesValidator =
        new Validator("times")
            .TwoArguments()
            .OneQuote()
            .IntegerAsSecond();

    public static readonly Validator TypeValidator =
        new Validator("type")
            .OneArgument();

    public static readonly Validator UndefValidator =
        new Validator("undef")
            .OneArgument()
            .SymbolOrStringOnTop();

    public static readonly Validator PrimrecValidator =
        new Validator("primrec")
            .ThreeArguments()
            .TwoQuotes()
            .AddRule(
                x => x[x.Count - 3].IsAggregate ||
                     x[x.Count - 3].IsOrdinal,
                "aggregate or ordinal");

    public readonly static Validator NameValidator =
        new Validator("name")
            .OneArgument();

    public readonly static Validator BodyValidator =
        new Validator("body")
            .OneArgument()
            .SymbolOnTop();

    public static readonly Validator UnitValidator =
        new Validator("unit")
            .OneArgument();

    public static readonly Validator SwapValidator =
        new Validator("swap")
            .TwoArguments();

    public static readonly Validator ConsValidator =
        new Validator("cons")
            .TwoArguments()
            .ListOnTop();

    public static readonly Validator InternValidator =
        new Validator("intern")
            .OneArgument()
            .StringOnTop();

    public static readonly Validator BranchValidator =
        new Validator("branch")
            .ThreeArguments()
            .TwoQuotes();

    public static readonly Validator ChoiceValidator =
        new Validator("choice")
            .ThreeArguments();

    public static readonly Validator IfteValidator =
        new Validator("ifte")
            .ThreeArguments()
            .ThreeQuotes();

    public static readonly Validator DipValidator =
        new Validator("dip")
            .TwoArguments()
            .OneQuote();

    public static readonly Validator ConcatValidator =
        new Validator("concat")
            .TwoArguments()
            .TwoAggregates();

    public static readonly Validator PopValidator =
        new Validator("pop")
            .OneArgument();

    public static readonly Validator FirstValidator =
        new Validator("first")
            .OneArgument()
            .AggregateOnTop();

    public static readonly Validator RestValidator =
        new Validator("rest")
            .OneArgument()
            .NonEmptyAggregateOnTop<IAggregate>();

    public static readonly Validator DupValidator =
        new Validator("dup")
            .OneArgument();

    public static readonly Validator _XValidator =
        new Validator("x")
            .OneArgument()
            .OneQuote();

    public static readonly Validator _IValidator =
        new Validator("i")
            .OneArgument()
            .OneQuote();

    public static readonly Validator UnstackValidator =
        new Validator("unstack")
            .OneArgument()
            .ListOnTop();

    public static readonly Validator SwaackValidator =
        new Validator("swaack")
            .OneArgument()
            .ListOnTop();

    public static readonly Validator InfraValidator =
        new Validator("infra")
            .TwoArguments()
            .TwoQuotes();

    public static readonly Validator MapValidator =
        new Validator("map")
            .TwoArguments()
            .OneQuote()
            .ListAsSecond();

    public static readonly Validator TraceValidator =
        new Validator("trace")
            .OneArgument()
            .OneQuote();
}
