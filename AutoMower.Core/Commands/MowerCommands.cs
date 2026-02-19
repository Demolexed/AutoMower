
using AutoMower.Core.Parser;

namespace AutoMower.Core.Commands;

public record MowerCommand (Lawn Lawn, IReadOnlyList<MowerSetup> Mowers);
