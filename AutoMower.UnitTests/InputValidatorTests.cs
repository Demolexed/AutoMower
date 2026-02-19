using AutoMower.Core.Parser;

namespace AutoMower.UnitTests;

public class InputValidatorTests
{
    private InputValidator _validator = null!;

    [SetUp]
    public void Setup()
    {
        _validator = new InputValidator();
    }

    [Test]
    public void Validate_ValidInput_ReturnsSuccess()
    {
        var input = new[]
        {
            "5 5",
            "1 2 N",
            "LFLFLFLFF"
        };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Empty);
    }

    [Test]
    public void Validate_ValidInputWithMultipleMowers_ReturnsSuccess()
    {
        var input = new[]
        {
            "5 5",
            "1 2 N",
            "LFLFLFLFF",
            "3 3 E",
            "FFRFFRFRRF"
        };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Empty);
    }

    [Test]
    public void Validate_EmptyInput_ReturnsError()
    {
        var input = Array.Empty<string>();

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Count.EqualTo(1));
        Assert.That(result.Errors[0], Does.Contain("vide"));
    }

    [Test]
    public void Validate_NullInput_ReturnsError()
    {
        var result = _validator.Validate(null!);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Count.EqualTo(1));
    }

    [Test]
    public void Validate_TooFewLines_ReturnsError()
    {
        var input = new[] { "5 5", "1 2 N" };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Count.GreaterThan(0));
        Assert.That(result.Errors[0], Does.Contain("minimum 3 lignes"));
    }

    [Test]
    public void Validate_EvenNumberOfLines_ReturnsError()
    {
        var input = new[]
        {
            "5 5",
            "1 2 N",
            "LFLFLFLFF",
            "3 3 E"
        };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.Contains("Nombre de lignes incorrect")), Is.True);
    }

    [Test]
    public void Validate_InvalidLawnFormat_MissingValue_ReturnsError()
    {
        var input = new[] { "5", "1 2 N", "F" };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.Contains("pelouse") && e.Contains("format incorrect")), Is.True);
    }

    [Test]
    public void Validate_InvalidLawnFormat_NonNumeric_ReturnsError()
    {
        var input = new[] { "ABC 5", "1 2 N", "F" };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.Contains("pelouse") && e.Contains("entier valide")), Is.True);
    }

    [Test]
    public void Validate_InvalidLawnFormat_NegativeValues_AcceptsValues()
    {
        var input = new[] { "-5 -3", "1 2 N", "F" };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Empty);
    }

    [Test]
    public void Validate_EmptyLawnLine_ReturnsError()
    {
        var input = new[] { "", "1 2 N", "F" };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.Contains("pelouse") && e.Contains("vide")), Is.True);
    }

    [Test]
    public void Validate_InvalidMowerPosition_MissingOrientation_ReturnsError()
    {
        var input = new[] { "5 5", "1 2", "F" };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.Contains("tondeuse #1") && e.Contains("format incorrect")), Is.True);
    }

    [Test]
    public void Validate_InvalidMowerPosition_NonNumericCoordinates_ReturnsError()
    {
        var input = new[] { "5 5", "A B N", "F" };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.Contains("tondeuse #1") && e.Contains("entier valide")), Is.True);
    }

    [Test]
    public void Validate_InvalidMowerPosition_InvalidOrientation_ReturnsError()
    {
        var input = new[] { "5 5", "1 2 X", "F" };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.Contains("tondeuse #1") && e.Contains("orientation valide")), Is.True);
    }

    [Test]
    public void Validate_EmptyMowerPositionLine_ReturnsError()
    {
        var input = new[] { "5 5", "", "F" };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.Contains("tondeuse #1") && e.Contains("position") && e.Contains("vide")), Is.True);
    }

    [Test]
    public void Validate_InvalidInstructions_InvalidCharacter_ReturnsError()
    {
        var input = new[] { "5 5", "1 2 N", "LFRX" };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.Contains("tondeuse #1") && e.Contains("instructions") && e.Contains("X")), Is.True);
    }

    [Test]
    public void Validate_EmptyInstructionsLine_ReturnsSuccess()
    {
        var input = new[] { "5 5", "1 2 N", "" };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Empty);
    }

    [Test]
    public void Validate_MissingInstructionsLine_ReturnsError()
    {
        var input = new[] { "5 5", "1 2 N" };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        // Soit erreur "minimum 3 lignes", soit "instructions manquante"
        Assert.That(result.Errors.Any(e => e.Contains("minimum 3 lignes") || e.Contains("instructions manquante")), Is.True);
    }

    [Test]
    public void Validate_MultipleMowersWithError_IdentifiesCorrectMowerNumber()
    {
        var input = new[]
        {
            "5 5",
            "1 2 N",
            "LFR",
            "3 3 Z",
            "FFR"
        };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.Contains("tondeuse #2")), Is.True);
    }

    [Test]
    public void Validate_MultipleErrors_ReturnsAllErrors()
    {
        var input = new[]
        {
            "ABC XYZ",
            "A B C",
            "XYZ"
        };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Count.GreaterThan(1));
    }

    [Test]
    public void Validate_FileFromAttachment_ReturnsSuccess()
    {
        var input = new[]
        {
            "5 5",
            "0 0 S",
            "F",
            "5 5 N",
            "F",
            "5 0 E",
            "F",
            "0 5 W",
            "F",
            "2 2 N",
            "FFFFFFFFFF",
            "3 3 E",
            "LLLLLLLLLL",
            "1 1 N",
            "RFRFRFRFRFRFRF",
            "4 4 S",
            "FFLFFRFFLFFRFF",
            "0 0 N",
            "LRLRLRLRLRLRLR"
        };

        var result = _validator.Validate(input);

        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Empty);
    }
}
