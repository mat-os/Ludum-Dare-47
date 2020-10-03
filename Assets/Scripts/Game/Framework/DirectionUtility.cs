namespace Game.Framework
{
    public class DirectionUtility
    {
        public static Direction GetDirection(Point target, Point current)
        {
            var fromPosition = current.transform.position;
            var targetPosition = target.transform.position;
            if (targetPosition.x > fromPosition.x)
            {
                return Direction.Right;
            }

            if (targetPosition.x < fromPosition.x)
            {
                return Direction.Left;
            }

            if (targetPosition.y < fromPosition.y)
            {
                return Direction.Down;
            }

            if (targetPosition.y > fromPosition.y)
            {
                return Direction.Up;
            }

            return Direction.None;
        }
    }
}