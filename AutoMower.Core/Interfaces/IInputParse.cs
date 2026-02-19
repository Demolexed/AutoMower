using AutoMower.Core.Parser;

namespace AutoMower.Core.Interfaces;

public interface IInputParse
{
    Tuple<Lawn, List<MowerSetup>> Parse(string[] lines);
}