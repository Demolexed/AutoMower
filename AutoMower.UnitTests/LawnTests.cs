using AutoMower.Core;
using AutoMower.Core.Enums;

namespace AutoMower.UnitTests;

public class LawnTests
{
    [Test]
    public void IsInside_WithValidPosition_ReturnsTrue()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(3, 4, Orientation.N);
        Assert.That(lawn.IsInside(position), Is.True);
    }

    [Test]
    public void IsInside_AtOrigin_ReturnsTrue()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(0, 0, Orientation.N);
        Assert.That(lawn.IsInside(position), Is.True);
    }

    [Test]
    public void IsInside_AtMaxBounds_ReturnsTrue()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(5, 5, Orientation.N);
        Assert.That(lawn.IsInside(position), Is.True);
    }

    [Test]
    public void IsInside_WithNegativeX_ReturnsFalse()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(-1, 3, Orientation.N);
        Assert.That(lawn.IsInside(position), Is.False);
    }

    [Test]
    public void IsInside_WithNegativeY_ReturnsFalse()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(3, -1, Orientation.N);
        Assert.That(lawn.IsInside(position), Is.False);
    }

    [Test]
    public void IsInside_WithXBeyondMax_ReturnsFalse()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(6, 3, Orientation.N);
        Assert.That(lawn.IsInside(position), Is.False);
    }

    [Test]
    public void IsInside_WithYBeyondMax_ReturnsFalse()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(3, 6, Orientation.N);
        Assert.That(lawn.IsInside(position), Is.False);
    }
}
