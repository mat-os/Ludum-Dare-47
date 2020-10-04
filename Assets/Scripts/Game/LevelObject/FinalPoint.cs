using Game.Framework;

namespace Game.LevelObject
{
    public class FinalPoint : Point
    {
        public override void Apply(Player player)
        {
            SoundController.Instance.FinalMusic();
            base.Apply(player);
        }
    }
}