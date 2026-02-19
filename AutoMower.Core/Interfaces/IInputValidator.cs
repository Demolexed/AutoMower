namespace AutoMower.Core.Interfaces;

public interface IInputValidator
{
    ValidationResult Validate(string[] lines);
}