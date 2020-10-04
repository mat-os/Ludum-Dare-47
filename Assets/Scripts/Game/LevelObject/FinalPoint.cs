using Game.Framework;
using UnityEngine;
using UnityEngine.Events;

namespace Game.LevelObject
{
    public class FinalPoint : Point
    {
        public Vector3 nextLevelCameraPosition;
        public UnityEvent finalEvent;

        public override void Apply(Player player)
        {
            SoundController.Instance.FinalMusic();

            var position = nextLevelCameraPosition;
            position.z = CameraController.Instance.transform.position.z;
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Kinoplenka");
            CameraController.Instance.GoToPosition(position, () => { finalEvent?.Invoke(); });
            base.Apply(player);
        }
    }
}