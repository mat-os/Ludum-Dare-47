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
        }

        public override void Apply(Player player)
        {
            if (capacity < maxCapacity)
            {
                var needCapacity = maxCapacity - capacity;
                var playerCapacity = Mathf.RoundToInt(player.Velocity);
                var chargeCapacity = Mathf.Min(needCapacity, playerCapacity);
                player.Velocity -= chargeCapacity;
                capacity += chargeCapacity;
                ReDraw();
            }
            else
            {
                base.Apply(player);
            }
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