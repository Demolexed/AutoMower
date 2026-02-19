using AutoMower.Core.Enums;
using AutoMower.Core.Interfaces;

namespace AutoMower.Core.Parser;

public class InputParser : IInputParse
{
    private readonly IInputValidator _validator;

    public InputParser(IInputValidator inputValidator)
    {
        _validator = inputValidator ?? throw new ArgumentNullException(nameof(inputValidator));
    }

    public Tuple<Lawn, List<MowerSetup>> Parse(string[] lines)
    {
        // Validation du format
        var validationResult = _validator.Validate(lines);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(Environment.NewLine, validationResult.Errors);
            throw new ArgumentException($"Format de fichier invalide:{Environment.NewLine}{errorMessage}");
        }

        var lawnParts = lines[0].Split(' ');
        var lawn = new Lawn(
            int.TryParse(lawnParts[0], out var lawnWidth) ? Math.Abs(lawnWidth) : throw new ArgumentException("Invalid lawn width"),
            int.TryParse(lawnParts[1], out var lawnHeight) ? Math.Abs(lawnHeight) : throw new ArgumentException("Invalid lawn height")
        );

        var mowerSetups = new List<MowerSetup>();

        for (int i = 1; i < lines.Length; i += 2)
        {
            var positionParts = lines[i].Split(' ');

            int x = int.Parse(positionParts[0]);
            int y = int.Parse(positionParts[1]);
            var orientation = Enum.Parse<Orientation>(positionParts[2]);

            var position = new Position(x, y, orientation);
            var instructions = lines[i + 1].Trim();

            mowerSetups.Add(new MowerSetup(position, instructions));
        }

        return Tuple.Create(lawn, mowerSetups);
    }
}
