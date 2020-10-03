using UnityEngine;

namespace DefaultNamespace
{
    public class Transistor : Point
    {
        public GameObject arrows;
        public float slowKoef;

        public override void BeforeApply(Player player)
        {
            base.BeforeApply(player);
            Time.timeScale /= slowKoef;
            arrows.SetActive(true);
        }

        public override void AfterApply(Player player)
        {
            base.AfterApply(player);
            Time.timeScale = 1;
            arrows.SetActive(false);
        }
    }
}