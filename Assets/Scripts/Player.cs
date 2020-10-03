using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        public float pointDistance = 0.1f;
        public float Velocity { get; set; }
        public Point StartPoint { get; set; }

        private int inPoint = 1;

        public Point NextPoint
        {
            get => _nextPoint;
            set
            {
                if (value == null)
                {
                    Velocity = 0;
                    Debug.LogError("Установлена пустая точка назначения");
                }

                _nextPoint = value;
            }
        }

        private Point _nextPoint;

        private void FixedUpdate()
        {
            if (Velocity > 0)
            {
                var targetPosition = _nextPoint.transform.position;
                transform.position = Vector3.MoveTowards(transform.position,
                    targetPosition, Velocity / 100);

                if (transform.position.Equals(targetPosition))
                {
                    _nextPoint.Apply(this);
                    inPoint = 0;
                }
                else if (Vector3.Distance(transform.position, targetPosition) < pointDistance && inPoint == 1)
                {
                    inPoint = -1;
                    _nextPoint.BeforeApply(this);
                }
                else if (Vector3.Distance(transform.position, StartPoint.transform.position) > pointDistance &&
                         inPoint == 0)
                {
                    inPoint = 1;
                    StartPoint.AfterApply(this);
                }
            }
        }
    }
}