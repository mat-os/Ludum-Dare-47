using UnityEngine;

namespace Game.LevelObject
{
    public class Сapacitor : Point
    {
        public int capacity = 1;
        public int maxCapacity = 5;
        public Transform lines;

        private int _currentCapacity;
        private void Awake()
        {
            _currentCapacity = capacity;
            ReDraw();
            Player.Instance.endGame += Reset;
        }

        private void Reset()
        {
            _currentCapacity = capacity;
            ReDraw();
        }

        public override void Apply(Player player)
        {
            if (_currentCapacity > 0)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Condensator");
                _currentCapacity--;
                player.Velocity++;
                ReDraw();
            }

            base.Apply(player);
        }

        private void ReDraw()
        {
            for (var i = 0; i < maxCapacity; i++)
            {
                lines.GetChild(i).gameObject.SetActive(i < _currentCapacity);
            }
        }
    }
}