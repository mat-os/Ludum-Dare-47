using DG.Tweening;
using UnityEngine;

namespace Game.Framework
{
    public class CameraController : MonoBehaviourSingleton<CameraController>
    {
        public float zoomDuration = 1;
        public float moveDuration = 2;
        public Camera localCamera;

        public void GoToPosition(Vector3 position)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(localCamera.DOFieldOfView(45, zoomDuration));
            sequence.Append(localCamera.transform.DOMove(position, moveDuration));
            sequence.Append(localCamera.DOFieldOfView(40, zoomDuration));
        }
    }
}