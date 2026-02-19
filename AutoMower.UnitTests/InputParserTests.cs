using AutoMower.Core.Enums;
using AutoMower.Core.Parser;

namespace AutoMower.UnitTests;

public class InputParserTests
{
    [Test]
    public void Parse_ValidInput_ParsesCorrectly()
    {
        var input = new[]
        {
            "5 5",
            "1 2 N",
            "LFLFLFLFF"
        };
        
        var parser = new InputParser();
        var result = parser.Parse(input);
        
        Assert.That(result.Item1.MaxX, Is.EqualTo(5));
        Assert.That(result.Item1.MaxY, Is.EqualTo(5));
        Assert.That(result.Item2.Count, Is.EqualTo(1));
        Assert.That(result.Item2[0].StartPosition.X, Is.EqualTo(1));
        Assert.That(result.Item2[0].StartPosition.Y, Is.EqualTo(2));
        Assert.That(result.Item2[0].StartPosition.Orientation, Is.EqualTo(Orientation.N));
        Assert.That(result.Item2[0].Instructions, Is.EqualTo("LFLFLFLFF"));
    }

    [Test]
    public void Parse_MultipleMowers_ParsesAll()
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
        var result = parser.Parse(input);
        
        Assert.That(result.Item2.Count, Is.EqualTo(2));
        Assert.That(result.Item2[0].StartPosition.X, Is.EqualTo(1));
        Assert.That(result.Item2[1].StartPosition.X, Is.EqualTo(3));
        Assert.That(result.Item2[1].StartPosition.Y, Is.EqualTo(3));
        Assert.That(result.Item2[1].StartPosition.Orientation, Is.EqualTo(Orientation.E));
    }

    [Test]
    public void Parse_EmptyInput_ThrowsException()
    {
        var input = Array.Empty<string>();
        var parser = new InputParser();
        
        var exception = Assert.Throws<ArgumentException>(() => parser.Parse(input));
        Assert.That(exception.Message, Does.Contain("Format de fichier invalide"));
    }

    [Test]
    public void Parse_InvalidLawnWidth_ThrowsException()
    {
        var input = new[] { "ABC 5", "1 1 N", "F" };
        var parser = new InputParser();
        
        var exception = Assert.Throws<ArgumentException>(() => parser.Parse(input));
        Assert.That(exception.Message, Does.Contain("Format de fichier invalide"));
    }

    [Test]
    public void Parse_NegativeLawnDimensions_ConvertsToAbsolute()
    {
        var input = new[]
        {
            "-5 -3",
            "1 1 N",
            "F"
        };
        
        var parser = new InputParser();
        var result = parser.Parse(input);
        
        Assert.That(result.Item1.MaxX, Is.EqualTo(5));
        Assert.That(result.Item1.MaxY, Is.EqualTo(3));
    }
}
