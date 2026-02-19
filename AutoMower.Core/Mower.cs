namespace AutoMower.Core
{
    public class Mower
    {
        public Position Position { get; private set; }
        private Lawn Lawn;

        public Mower(Position position, Lawn lawn)
        {
            Position = position;
            Lawn = lawn;
        }

        private void MoveForward()
        {
            var nextPosition = Position.MoveForward();

            if (Lawn.IsInside(nextPosition))
                Position = nextPosition;
        }

        private void TurnLeft()
        {
            Position = Position.TurnLeft();
        }

        private void TurnRight()
        {
            Position = Position.TurnRight();
        }

        public void Execute(string instructions)
        {
            foreach (var command in instructions)
            {
                switch (command)
                {
                    case 'L':
                        TurnLeft();
                        break;
                    case 'R':
                        TurnRight();
                        break;
                    case 'F':
                        MoveForward();
                        break;
                    default:
                        throw new InvalidOperationException($"Commande inconnue: {command}");
                }
            }
        }
    }
}
