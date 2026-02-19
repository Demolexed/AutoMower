using AutoMower.Core.Enums;
using AutoMower.Core.Interfaces;

namespace AutoMower.Core.Parser;

public class InputValidator : IInputValidator
{
    public ValidationResult Validate(string[] lines)
    {
        var errors = new List<string>();

        if (lines == null || lines.Length == 0)
        {
            errors.Add("Le fichier est vide");
            return new ValidationResult(false, errors);
        }

        if (lines.Length < 3)
        {
            errors.Add("Le fichier doit contenir au minimum 3 lignes (1 ligne pelouse + au moins 1 tondeuse)");
            return new ValidationResult(false, errors);
        }

        if (lines.Length % 2 == 0)
        {
            errors.Add($"Nombre de lignes incorrect ({lines.Length}). Format attendu: 1 ligne pelouse + paires de lignes pour chaque tondeuse");
            return new ValidationResult(false, errors);
        }

        ValidateLawnDimensions(lines[0], errors);

        int mowerNumber = 1;
        for (int i = 1; i < lines.Length; i += 2)
        {
            if (i + 1 >= lines.Length)
            {
                errors.Add($"Tondeuse #{mowerNumber}: ligne d'instructions manquante");
                break;
            }

            ValidateMowerPosition(lines[i], mowerNumber, errors);
            ValidateMowerInstructions(lines[i + 1], mowerNumber, errors);
            mowerNumber++;
        }

        return new ValidationResult(errors.Count == 0, errors);
    }

    private void ValidateLawnDimensions(string line, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            errors.Add("Ligne 1 (pelouse): la ligne est vide");
            return;
        }

        var parts = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
        {
            errors.Add($"Ligne 1 (pelouse): format incorrect. Attendu: 'X Y' (2 entiers), reçu: '{line}'");
            return;
        }

        if (!int.TryParse(parts[0], out _))
        {
            errors.Add($"Ligne 1 (pelouse): '{parts[0]}' n'est pas un entier valide pour la largeur");
        }

        if (!int.TryParse(parts[1], out _))
        {
            errors.Add($"Ligne 1 (pelouse): '{parts[1]}' n'est pas un entier valide pour la hauteur");
        }
    }

    private void ValidateMowerPosition(string line, int mowerNumber, List<string> errors)
    {
        int lineNumber = 1 + (mowerNumber - 1) * 2 + 1;

        if (string.IsNullOrWhiteSpace(line))
        {
            errors.Add($"Ligne {lineNumber} (tondeuse #{mowerNumber} position): la ligne est vide");
            return;
        }

        var parts = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 3)
        {
            errors.Add($"Ligne {lineNumber} (tondeuse #{mowerNumber} position): format incorrect. Attendu: 'X Y O' (2 entiers + orientation), reçu: '{line}'");
            return;
        }

        if (!int.TryParse(parts[0], out var x))
        {
            errors.Add($"Ligne {lineNumber} (tondeuse #{mowerNumber} position): '{parts[0]}' n'est pas un entier valide pour X");
        }

        if (!int.TryParse(parts[1], out var y))
        {
            errors.Add($"Ligne {lineNumber} (tondeuse #{mowerNumber} position): '{parts[1]}' n'est pas un entier valide pour Y");
        }

        if (!Enum.TryParse<Orientation>(parts[2], out _))
        {
            errors.Add($"Ligne {lineNumber} (tondeuse #{mowerNumber} position): '{parts[2]}' n'est pas une orientation valide. Valeurs acceptées: N, S, E, W");
        }
    }

    private void ValidateMowerInstructions(string line, int mowerNumber, List<string> errors)
    {
        int lineNumber = 1 + (mowerNumber - 1) * 2 + 2;

        if (line == null)
        {
            errors.Add($"Ligne {lineNumber} (tondeuse #{mowerNumber} instructions): la ligne est nulle");
            return;
        }

        var instructions = line.Trim();

        if (string.IsNullOrEmpty(instructions))
        {
            return;
        }

        var validCommands = new HashSet<char> { 'L', 'R', 'F' };

        for (int i = 0; i < instructions.Length; i++)
        {
            if (!validCommands.Contains(instructions[i]))
            {
                errors.Add($"Ligne {lineNumber} (tondeuse #{mowerNumber} instructions): caractère invalide '{instructions[i]}' à la position {i + 1}. Commandes valides: L, R, F");
            }
        }
    }
}
