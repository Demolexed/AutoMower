using AutoMower.Core;
using AutoMower.Core.Enums;

namespace AutoMower.UnitTests;

public class PositionTests
{
    [Test]
    public void MoveForward_FromNorth_IncreasesY()
    {
        var position = new Position(2, 3, Orientation.N);
        var newPosition = position.MoveForward();
        Assert.That(newPosition.X, Is.EqualTo(2));
        Assert.That(newPosition.Y, Is.EqualTo(4));
        Assert.That(newPosition.Orientation, Is.EqualTo(Orientation.N));
    }

    [Test]
    public void MoveForward_FromSouth_DecreasesY()
    {
        var position = new Position(2, 3, Orientation.S);
        var newPosition = position.MoveForward();
        Assert.That(newPosition.X, Is.EqualTo(2));
        Assert.That(newPosition.Y, Is.EqualTo(2));
        Assert.That(newPosition.Orientation, Is.EqualTo(Orientation.S));
    }

    [Test]
    public void MoveForward_FromEast_IncreasesX()
    {
        var position = new Position(2, 3, Orientation.E);
        var newPosition = position.MoveForward();
        Assert.That(newPosition.X, Is.EqualTo(3));
        Assert.That(newPosition.Y, Is.EqualTo(3));
        Assert.That(newPosition.Orientation, Is.EqualTo(Orientation.E));
    }

    [Test]
    public void MoveForward_FromWest_DecreasesX()
    {
        var position = new Position(2, 3, Orientation.W);
        var newPosition = position.MoveForward();
        Assert.That(newPosition.X, Is.EqualTo(1));
        Assert.That(newPosition.Y, Is.EqualTo(3));
        Assert.That(newPosition.Orientation, Is.EqualTo(Orientation.W));
    }

    [Test]
    public void TurnLeft_FromNorth_TurnsToWest()
    {
        var position = new Position(2, 3, Orientation.N);
        var newPosition = position.TurnLeft();
        Assert.That(newPosition.Orientation, Is.EqualTo(Orientation.W));
    }

    [Test]
    public void TurnLeft_FromWest_TurnsToSouth()
    {
        var position = new Position(2, 3, Orientation.W);
        var newPosition = position.TurnLeft();
        Assert.That(newPosition.Orientation, Is.EqualTo(Orientation.S));
    }

    [Test]
    public void TurnLeft_FromSouth_TurnsToEast()
    {
        var position = new Position(2, 3, Orientation.S);
        var newPosition = position.TurnLeft();
        Assert.That(newPosition.Orientation, Is.EqualTo(Orientation.E));
    }

    [Test]
    public void TurnLeft_FromEast_TurnsToNorth()
    {
        var position = new Position(2, 3, Orientation.E);
        var newPosition = position.TurnLeft();
        Assert.That(newPosition.Orientation, Is.EqualTo(Orientation.N));
    }

    [Test]
    public void TurnRight_FromNorth_TurnsToEast()
    {
        var position = new Position(2, 3, Orientation.N);
        var newPosition = position.TurnRight();
        Assert.That(newPosition.Orientation, Is.EqualTo(Orientation.E));
    }

    [Test]
    public void TurnRight_FromEast_TurnsToSouth()
    {
        var position = new Position(2, 3, Orientation.E);
        var newPosition = position.TurnRight();
        Assert.That(newPosition.Orientation, Is.EqualTo(Orientation.S));
    }

    [Test]
    public void TurnRight_FromSouth_TurnsToWest()
    {
        var position = new Position(2, 3, Orientation.S);
        var newPosition = position.TurnRight();
        Assert.That(newPosition.Orientation, Is.EqualTo(Orientation.W));
    }

    [Test]
    public void TurnRight_FromWest_TurnsToNorth()
    {
        var position = new Position(2, 3, Orientation.W);
        var newPosition = position.TurnRight();
        Assert.That(newPosition.Orientation, Is.EqualTo(Orientation.N));
    }

    [Test]
    public void FourLeftTurns_ReturnsToOriginalOrientation()
    {
        var position = new Position(0, 0, Orientation.N);
        var result = position.TurnLeft().TurnLeft().TurnLeft().TurnLeft();
        Assert.That(result.Orientation, Is.EqualTo(Orientation.N));
    }

    [Test]
    public void FourRightTurns_ReturnsToOriginalOrientation()
    {
        var position = new Position(0, 0, Orientation.N);
        var result = position.TurnRight().TurnRight().TurnRight().TurnRight();
        Assert.That(result.Orientation, Is.EqualTo(Orientation.N));
    }
}
