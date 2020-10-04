using Game.Framework;
using UnityEngine;

namespace Game.LevelObject
{
    public class FinalPoint : Point
    {
        public Vector3 nextLevelCameraPosition;

        public override void Apply(Player player)
        {
            SoundController.Instance.FinalMusic();

            var position = nextLevelCameraPosition;
            position.z = CameraController.Instance.transform.position.z;
            CameraController.Instance.GoToPosition(position);

            base.Apply(player);
        }
    }
}