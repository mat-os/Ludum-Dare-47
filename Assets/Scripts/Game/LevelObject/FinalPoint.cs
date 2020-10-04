using Game.Framework;
using UnityEngine;

namespace Game.LevelObject
{
    public class FinalPoint : Point
    {
        public Transform nextLevelCameraPosition;
        public override void Apply(Player player)
        {
            SoundController.Instance.FinalMusic();
            if (nextLevelCameraPosition != null)
            {
                var position = nextLevelCameraPosition.position;
                position.z = CameraController.Instance.transform.position.z;
                CameraController.Instance.GoToPosition(position);
            }
            
            base.Apply(player);
        }
    }
}