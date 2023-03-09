namespace Kay;

/// <summary>
/// Provides some values to /tag/ values to provide a bit more distribution
/// in their hashcodes.
/// </summary>
/// <remarks>
/// These values are plucked at random from a list of prime numbers.
/// </remarks>
public static class HashTags
{
    public const int Boolean = 13;

    public const int Char = 37;

    public const int Integer = 73;

    public const int Float = 661;

    public const int List = 907;

    public const int Set = 409;

    public const int String = 1087;

    public const int Symbol = 1381;
}