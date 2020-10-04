using UnityEngine;

namespace Game.LevelObject
{
    public class Сapacitor : Point
    {
        public int capacity = 1;
        public int maxCapacity = 5;
        public Transform lines;

        private void Awake()
        {
            ReDraw();
            Player.Instance.endGame += Reset;
        }

        private void Reset()
        {
            capacity = 1;
            ReDraw();
        }

        public override void Apply(Player player)
        {
            if (capacity > 0)
            {
                capacity--;
                player.Velocity++;
                ReDraw();
            }

            base.Apply(player);
        }

        private void ReDraw()
        {
            for (var i = 0; i < maxCapacity; i++)
            {
                lines.GetChild(i).gameObject.SetActive(i < capacity);
            }
        }
    }
}