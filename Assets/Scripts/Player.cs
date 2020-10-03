using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        public float Velocity { get; set; }
        public Point StartPoint { get; set; }

        public Point NextPoint
        {
            get => _nextPoint;
            set
            {
                if (value == null)
                {
                    Debug.LogError("Установлена пустая точка назначения");
                }
                StartPoint = value;
                _nextPoint = value;
            }
        }

        private Point _nextPoint;

        private void FixedUpdate()
        {
            if (Velocity > 0)
            {
                Vector3.MoveTowards(transform.position,
                    _nextPoint.transform.position, Velocity);
            }
        }
    }
}