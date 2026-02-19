using AutoMower.Core.Commands;
using AutoMower.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMower.Core.Services;

public class MowerService : IMowerService
{
    public IReadOnlyList<Position> Execute(MowerCommand command)
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        var results = new List<Position>(command.Mowers.Count);

        foreach (var setup in command.Mowers)
        {
            var mower = new Mower(setup.StartPosition, command.Lawn);
            mower.Execute(setup.Instructions);
            results.Add(mower.Position);
        }

        return results;
    }
}
