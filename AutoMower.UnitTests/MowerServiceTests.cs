using AutoMower.Core;
using AutoMower.Core.Commands;
using AutoMower.Core.Enums;
using AutoMower.Core.Parser;
using AutoMower.Core.Services;

namespace AutoMower.UnitTests;

public class MowerServiceTests
{
    [Test]
    public void Execute_SingleMower_ReturnsCorrectPosition()
    {
        var lawn = new Lawn(5, 5);
        var mowerSetup = new MowerSetup(new Position(1, 2, Orientation.N), "LFLFLFLFF");
        var command = new MowerCommand(lawn, new List<MowerSetup> { mowerSetup });
        
        var service = new MowerService();
        var results = service.Execute(command);
        
        Assert.That(results.Count, Is.EqualTo(1));
        Assert.That(results[0].X, Is.EqualTo(1));
        Assert.That(results[0].Y, Is.EqualTo(3));
        Assert.That(results[0].Orientation, Is.EqualTo(Orientation.N));
    }

    [Test]
    public void Execute_MultipleMowers_ReturnsAllPositions()
    {
        var lawn = new Lawn(5, 5);
        var mowers = new List<MowerSetup>
        {
            new MowerSetup(new Position(1, 2, Orientation.N), "LFLFLFLFF"),
            new MowerSetup(new Position(3, 3, Orientation.E), "FFRFFRFRRF")
        };
        var command = new MowerCommand(lawn, mowers);
        
        var service = new MowerService();
        var results = service.Execute(command);
        
        Assert.That(results.Count, Is.EqualTo(2));
        Assert.That(results[0].X, Is.EqualTo(1));
        Assert.That(results[0].Y, Is.EqualTo(3));
        Assert.That(results[1].X, Is.EqualTo(5));
        Assert.That(results[1].Y, Is.EqualTo(1));
    }

    [Test]
    public void Execute_NullCommand_ThrowsException()
    {
        var service = new MowerService();
        Assert.Throws<ArgumentNullException>(() => service.Execute(null!));
    }
}
