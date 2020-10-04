using UnityEngine;

namespace Game.LevelObject
{
    public class Resister : Point
    {
        public int slowValue = 1;
        public Transform lines;

        private void Awake()
        {
            for (var i = 0; i < slowValue; i++)
            {
                lines.GetChild(i).gameObject.SetActive(true);
            }
        }

        public override void Apply(Player player)
        {
            base.Apply(player);
            player.Velocity -= slowValue;
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Resistor");
        }
    }
}