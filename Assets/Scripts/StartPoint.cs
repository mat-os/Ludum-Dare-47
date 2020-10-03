using UnityEngine;

namespace DefaultNamespace
{
    public class StartPoint : Point
    {
        public float startVelocity = 5;

        private Player _player;

        public override void Apply(Player player)
        {
            _player = player;
        }

        private void Update()
        {
            if (_player.StartPoint == null && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            {
                Launch();
            }
        }

        private void Launch()
        {
            _player.StartPoint = this;
            _player.NextPoint = GetNextPoint(this);
            _player.Velocity = startVelocity;
        }
    }
}