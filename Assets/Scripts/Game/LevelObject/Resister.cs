namespace Game.LevelObject
{
    public class Resister : Point
    {
        public int slowValue = 1;

        public override void Apply(Player player)
        {
            base.Apply(player);
            player.Velocity -= slowValue;
        }
    }
}