using AutoMower.Core.Commands;

namespace AutoMower.Core.Interfaces
{
    public interface IMowerService
    {
        IReadOnlyList<Position> Execute(MowerCommand command);
    }
}