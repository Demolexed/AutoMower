using AutoMower.Core.Enums;

namespace AutoMower.Core;

public record Position
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Orientation Orientation { get; private set; }

    public Position(int x, int y, Orientation orientation)
    {
        X = x;
        Y = y;
        Orientation = orientation;
    }

    public Position MoveForward()
    {
        return Orientation switch
        {
            Orientation.N => this with { Y = Y + 1 },
            Orientation.S => this with { Y = Y - 1 },
            Orientation.E => this with { X = X + 1 },
            Orientation.W => this with { X = X - 1 },
            _ => this
        };
    }

    public Position TurnLeft()
    {
        return Orientation switch
        {
            Orientation.N => this with { Orientation = Orientation.W },
            Orientation.W => this with { Orientation = Orientation.S },
            Orientation.S => this with { Orientation = Orientation.E },
            Orientation.E => this with { Orientation = Orientation.N },
            _ => this
        };
    }

    public Position TurnRight()
    {
        return Orientation switch
        {
            Orientation.N => this with { Orientation = Orientation.E },
            Orientation.E => this with { Orientation = Orientation.S },
            Orientation.S => this with { Orientation = Orientation.W },
            Orientation.W => this with { Orientation = Orientation.N },
            _ => this
        };
    }
}
