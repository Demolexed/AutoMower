using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMower.Core;

public class Lawn
{
    public int MaxX { get; }
    public int MaxY { get; }

    public Lawn(int maxX, int maxY)
    {
        MaxX = maxX;
        MaxY = maxY;
    }

    public bool IsInside(Position position)
    {
        return position.X >= 0 &&
               position.X <= MaxX &&
               position.Y >= 0 &&
               position.Y <= MaxY;
    }
}
