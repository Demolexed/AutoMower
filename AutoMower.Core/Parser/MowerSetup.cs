namespace AutoMower.Core.Parser;

public class MowerSetup
{
    public Position StartPosition { get; }
    public string Instructions { get; }

    public MowerSetup(Position startPosition, string instructions)
    {
        StartPosition = startPosition;
        Instructions = instructions;
    }
}
