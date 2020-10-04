using System;
using UnityEngine;


public class Player : MonoBehaviourSingleton<Player>
{
    public float pointDistance = 0.1f;
    public float animateDistance = 0.3f;
    public float speedKoef = 1f;
    public event Action endGame;
    public Animator playerAnimator;
    public StartPoint beginPoint;

    private void Awake()
    {
        StartPoint = beginPoint;
    }

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

    public Point NextPoint
    {
        get => _nextPoint;
        set
        {
            _nextPoint = value;
            if (value == null)
            {
                Debug.LogError("Установлена пустая точка назначения");
                Restart();
            }
            else
            {
                _nextPoint.SetAsNext(this);
            }
        }
    }

    private Point _nextPoint;
    private static readonly int ToRight = Animator.StringToHash("ToRight");
    private static readonly int VelocityParam = Animator.StringToHash("Velocity");
    private static readonly float TOLERANCE = 0.0001f;
    private static readonly int ToLeft = Animator.StringToHash("ToLeft");

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
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/PlayerDamaged");
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
                if (nextPoint != null)
                {
                    var angle = Vector2.SignedAngle(transform.up,
                        nextPoint.transform.position - _nextPoint.transform.position);
                    animating = true;
                    if (Math.Abs(angle + 90) < TOLERANCE)
                    {
                        playerAnimator.SetTrigger(ToRight);
                        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Povorot");
                    }
                    else if (Math.Abs(angle - 90) < TOLERANCE)
                    {
                        playerAnimator.SetTrigger(ToLeft);
                        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Povorot");
                    }
                }
            }

            if (transform.position.Equals(targetPosition) && inPoint != 0)
            {
                _nextPoint.Apply(this);
                inPoint = 0;
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