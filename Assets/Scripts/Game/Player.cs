using System;
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour
{
    public float pointDistance = 0.1f;
    public float animateDistance = 0.3f;
    public float speedKoef = 1f;
    public UnityEvent endGame;
    public Animator playerAnimator;

    public float Velocity
    {
        get => _velocity;
        set
        {
            if (_velocity != value)
            {
                _velocity = Mathf.Clamp(value, 0, MaxVelocity);
                playerAnimator.SetFloat(VelocityParam, Velocity);
                if (_velocity == 0)
                {
                    Restart();
                } 
            }
        }
    }

    public Point PrevPoint { get; set; }
    public StartPoint StartPoint { get; set; }

    private float _velocity;
    public int MaxVelocity = 5;
    private int inPoint = 1;

    private bool animating;

    //убрать после добавления ивента в анимацию
    private bool delayed;

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
            _nextPoint.SetAsNext(this);
        }
    }

    private Point _nextPoint;
    private static readonly int ToRight = Animator.StringToHash("ToRight");
    private static readonly int VelocityParam = Animator.StringToHash("Velocity");
    private static readonly float TOLERANCE = 0.0001f;

    public void TurnRight()
    {
        var eulerAngles = transform.eulerAngles;
        eulerAngles.z -= 90;
        transform.eulerAngles = eulerAngles;
        animating = false;
    }

    public void TurnLeft()
    {
        var eulerAngles = transform.eulerAngles;
        eulerAngles.z += 90;
        transform.eulerAngles = eulerAngles;
        animating = false;
    }

    public void Restart()
    {
        endGame?.Invoke();
        StartPoint.Prepare();
    }

    private void FixedUpdate()
    {
        if (Velocity > 0)
        {
            var targetPosition = _nextPoint.transform.position;
            transform.position = Vector3.MoveTowards(transform.position,
                targetPosition, Velocity * speedKoef / 50);
            if (!animating && Vector3.Distance(transform.position, targetPosition) < animateDistance)
            {
                var nextPoint = _nextPoint.GetNextPoint(PrevPoint);
                var angle = Vector2.SignedAngle(transform.up,
                    nextPoint.transform.position - _nextPoint.transform.position);
                animating = true;
                if (Math.Abs(angle + 90) < TOLERANCE)
                {
                    playerAnimator.SetTrigger(ToRight);
                }
                else if (Math.Abs(angle - 90) < TOLERANCE)
                {
                    //Пока нет анимации просто поворачиваю объект
                    delayed = true;
                }
            }

            if (transform.position.Equals(targetPosition))
            {
                _nextPoint.Apply(this);
                inPoint = 0;
                //убрать после добавления анимации
                if (delayed)
                {
                    delayed = false;
                    TurnLeft();
                }

                animating = false;
            }
            else if (Vector3.Distance(transform.position, targetPosition) < pointDistance && inPoint == 1)
            {
                inPoint = -1;
                _nextPoint.BeforeApply(this);
            }
            else if (Vector3.Distance(transform.position, PrevPoint.transform.position) > pointDistance &&
                     inPoint == 0)
            {
                inPoint = 1;
                PrevPoint.AfterApply(this);
            }
        }
    }
}