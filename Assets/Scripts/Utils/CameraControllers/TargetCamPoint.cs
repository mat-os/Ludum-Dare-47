using DG.Tweening;
using UnityEngine;

namespace Script.Utils.CameraControllers
{
    public class TargetCamPoint : MonoBehaviour
    {
        public float Fov = 50f;
        public float TimeToChange = 1f;
        public Ease ChangeEase;

        [Header("Target")]
        public GameObject Target;
        
        public bool LookOnTarget;
        
        public bool FollowTarget;
        public float FollowMovementSmoother;
        public bool SmoothSetupOnPosition;
        
        private Vector3 _startOffset;
        private Vector3 _startRotation;

        public Vector3 StartOffset => _startOffset;
        private void Start()
        {
            if (Target != null)
            {
                _startRotation = transform.rotation.eulerAngles;
                _startOffset = Target.transform.position - transform.position;
            }
        }
        private void LateUpdate()
        {
            if (FollowTarget)
            {
                var currentPos = transform.position;
                var newPos = currentPos;
                newPos.x = Target.transform.position.x - _startOffset.x;
                
               transform.position = newPos;
            }
        }

        private void OnDrawGizmosSelected()
        {
            var totalFOV = 70.0f;
            var rayRange = 10.0f;
            var halfFOV = totalFOV / 2.0f;
            var leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
            var rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
            var leftRayDirection = leftRayRotation * transform.forward;
            var rightRayDirection = rightRayRotation * transform.forward;
            Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
            Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
        }
    }
}