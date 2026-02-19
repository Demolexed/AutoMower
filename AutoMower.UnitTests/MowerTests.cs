using AutoMower.Core;
using AutoMower.Core.Enums;

namespace AutoMower.UnitTests;

public class MowerTests
{
    [Test]
    public void Execute_SimpleForward_MovesCorrectly()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(0, 0, Orientation.N);
        var mower = new Mower(position, lawn);
        
        mower.Execute("F");
        
        Assert.That(mower.Position.X, Is.EqualTo(0));
        Assert.That(mower.Position.Y, Is.EqualTo(1));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.N));
    }

    [Test]
    public void Execute_TurnLeftAndForward_MovesCorrectly()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(1, 1, Orientation.N);
        var mower = new Mower(position, lawn);
        
        mower.Execute("LF");
        
        Assert.That(mower.Position.X, Is.EqualTo(0));
        Assert.That(mower.Position.Y, Is.EqualTo(1));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.W));
    }

    [Test]
    public void Execute_TurnRightAndForward_MovesCorrectly()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(1, 1, Orientation.N);
        var mower = new Mower(position, lawn);
        
        mower.Execute("RF");
        
        Assert.That(mower.Position.X, Is.EqualTo(2));
        Assert.That(mower.Position.Y, Is.EqualTo(1));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.E));
    }

    [Test]
    public void Execute_ComplexInstructions_MovesCorrectly()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(1, 2, Orientation.N);
        var mower = new Mower(position, lawn);
        
        mower.Execute("LFLFLFLFF");
        
        Assert.That(mower.Position.X, Is.EqualTo(1));
        Assert.That(mower.Position.Y, Is.EqualTo(3));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.N));
    }

    [Test]
    public void Execute_AtBoundary_DoesNotMoveOutside()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(0, 0, Orientation.W);
        var mower = new Mower(position, lawn);
        
        mower.Execute("F");
        
        Assert.That(mower.Position.X, Is.EqualTo(0));
        Assert.That(mower.Position.Y, Is.EqualTo(0));
    }

    [Test]
    public void Execute_MultipleForwardAtBoundary_StaysInside()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(5, 5, Orientation.N);
        var mower = new Mower(position, lawn);
        
        mower.Execute("FFF");
        
        Assert.That(mower.Position.X, Is.EqualTo(5));
        Assert.That(mower.Position.Y, Is.EqualTo(5));
    }

    [Test]
    public void Execute_InvalidCommand_ThrowsException()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(0, 0, Orientation.N);
        var mower = new Mower(position, lawn);
        
        Assert.Throws<InvalidOperationException>(() => mower.Execute("X"));
    }

    [Test]
    public void Execute_EmptyInstructions_DoesNotMove()
    {
        var lawn = new Lawn(5, 5);
        var position = new Position(2, 3, Orientation.E);
        var mower = new Mower(position, lawn);
        
        mower.Execute("");
        
        Assert.That(mower.Position.X, Is.EqualTo(2));
        Assert.That(mower.Position.Y, Is.EqualTo(3));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.E));
    }
}
