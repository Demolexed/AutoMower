using AutoMower.Core;
using AutoMower.Core.Commands;
using AutoMower.Core.Enums;
using AutoMower.Core.Parser;
using AutoMower.Core.Services;

namespace AutoMower.UnitTests;

public class IntegrationTests
{
    [Test]
    public void TestCase1_From00S_WithF_Returns00N()
    {
        var lawn = new Lawn(5, 5);
        var mower = new Mower(new Position(0, 0, Orientation.S), lawn);
        mower.Execute("F");
        
        Assert.That(mower.Position.X, Is.EqualTo(0));
        Assert.That(mower.Position.Y, Is.EqualTo(0));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.S));
    }

    [Test]
    public void TestCase2_From55N_WithF_Returns55N()
    {
        var lawn = new Lawn(5, 5);
        var mower = new Mower(new Position(5, 5, Orientation.N), lawn);
        mower.Execute("F");
        
        Assert.That(mower.Position.X, Is.EqualTo(5));
        Assert.That(mower.Position.Y, Is.EqualTo(5));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.N));
    }

    [Test]
    public void TestCase3_From50E_WithF_Returns50E()
    {
        var lawn = new Lawn(5, 5);
        var mower = new Mower(new Position(5, 0, Orientation.E), lawn);
        mower.Execute("F");
        
        Assert.That(mower.Position.X, Is.EqualTo(5));
        Assert.That(mower.Position.Y, Is.EqualTo(0));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.E));
    }

    [Test]
    public void TestCase4_From05W_WithF_Returns05W()
    {
        var lawn = new Lawn(5, 5);
        var mower = new Mower(new Position(0, 5, Orientation.W), lawn);
        mower.Execute("F");
        
        Assert.That(mower.Position.X, Is.EqualTo(0));
        Assert.That(mower.Position.Y, Is.EqualTo(5));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.W));
    }

    [Test]
    public void TestCase5_From22N_With10F_Returns27N()
    {
        var lawn = new Lawn(5, 5);
        var mower = new Mower(new Position(2, 2, Orientation.N), lawn);
        mower.Execute("FFFFFFFFFF");
        
        Assert.That(mower.Position.X, Is.EqualTo(2));
        Assert.That(mower.Position.Y, Is.EqualTo(5));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.N));
    }

    [Test]
    public void TestCase6_From33E_With10L_Returns33W()
    {
        var lawn = new Lawn(5, 5);
        var mower = new Mower(new Position(3, 3, Orientation.E), lawn);
        mower.Execute("LLLLLLLLLL");
        
        Assert.That(mower.Position.X, Is.EqualTo(3));
        Assert.That(mower.Position.Y, Is.EqualTo(3));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.W));
    }

    [Test]
    public void TestCase7_From11N_WithRFRFRFRFRFRFRF_Returns10W()
    {
        var lawn = new Lawn(5, 5);
        var mower = new Mower(new Position(1, 1, Orientation.N), lawn);
        mower.Execute("RFRFRFRFRFRFRF");
        
        Assert.That(mower.Position.X, Is.EqualTo(1));
        Assert.That(mower.Position.Y, Is.EqualTo(0));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.W));
    }

    [Test]
    public void TestCase8_From44S_WithComplexPattern_ReturnsCorrectPosition()
    {
        var lawn = new Lawn(5, 5);
        var mower = new Mower(new Position(4, 4, Orientation.S), lawn);
        mower.Execute("FFLFFRFFLFFRFF");
        
        Assert.That(mower.Position.X, Is.EqualTo(5));
        Assert.That(mower.Position.Y, Is.EqualTo(0));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.S));
    }

    [Test]
    public void TestCase9_From00N_WithAlternatingLR_Returns00N()
    {
        var lawn = new Lawn(5, 5);
        var mower = new Mower(new Position(0, 0, Orientation.N), lawn);
        mower.Execute("LRLRLRLRLRLRLR");
        
        Assert.That(mower.Position.X, Is.EqualTo(0));
        Assert.That(mower.Position.Y, Is.EqualTo(0));
        Assert.That(mower.Position.Orientation, Is.EqualTo(Orientation.N));
    }

    [Test]
    public void FullIntegration_WithFileContent_ExecutesCorrectly()
    {
        var input = new[]
        {
            "5 5",
            "1 2 N",
            "LFLFLFLFF",
            "3 3 E",
            "FFRFFRFRRF"
        };
        
        var parser = new InputParser();
        var parsed = parser.Parse(input);
        var command = new MowerCommand(parsed.Item1, parsed.Item2);
        
        var service = new MowerService();
        var results = service.Execute(command);
        
        Assert.That(results.Count, Is.EqualTo(2));
        Assert.That(results[0].X, Is.EqualTo(1));
        Assert.That(results[0].Y, Is.EqualTo(3));
        Assert.That(results[0].Orientation, Is.EqualTo(Orientation.N));
        Assert.That(results[1].X, Is.EqualTo(5));
        Assert.That(results[1].Y, Is.EqualTo(1));
        Assert.That(results[1].Orientation, Is.EqualTo(Orientation.E));
    }
}
