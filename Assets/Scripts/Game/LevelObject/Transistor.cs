using System;
using System.Collections.Generic;
using System.Linq;
using Game.Framework;
using Game.LevelObject;
using UnityEngine;

public class Transistor : Point
{
    public float slowKoef = 1;
    public Arrow up;
    public Arrow down;
    public Arrow left;
    public Arrow right;

    private Direction _direction = Direction.None;
    private readonly Dictionary<Point, Arrow> _arrows = new Dictionary<Point, Arrow>();
    private bool _enableControl;
    private bool _slowed;

    private void Awake()
    {
        up.SetActive(false);
        down.SetActive(false);
        right.SetActive(false);
        left.SetActive(false);

        var fromPosition = transform.position;
        _connections.ForEach(point =>
        {
            var targetPosition = point.transform.position;
            if (targetPosition.x > fromPosition.x)
            {
                _arrows.Add(point, right);
            }
            else if (targetPosition.x < fromPosition.x)
            {
                _arrows.Add(point, left);
            }
            else if (targetPosition.y < fromPosition.y)
            {
                _arrows.Add(point, down);
            }
            else if (targetPosition.y > fromPosition.y)
            {
                _arrows.Add(point, up);
            }
        });
    }

    public override void BeforeApply(Player player)
    {
        base.BeforeApply(player);
        if (_direction == Direction.None)
        {
            _slowed = true;
            _player.Velocity /= slowKoef;
        }
    }

    public override void Apply(Player player)
    {
        base.Apply(player);
        if (_slowed)
        {
            _slowed = false;
            player.Velocity *= slowKoef;
        }
        
        foreach (var arrow in _arrows.Values)
        {
            arrow.SetActive(false);
        }

        _enableControl = false;
    }

    public override void SetAsNext(Player player)
    {
        var exits = _connections.Where(point => point != player.PrevPoint).ToList();
        var nextPoint = base.GetNextPoint(player.PrevPoint);
        ClearColors();
        exits.ForEach(point =>
        {
            if (_arrows.ContainsKey(point))
            {
                _arrows[point].SetActive(true);
//                if (nextPoint == point)
//                {
//                    _arrows[point].SetColor(Color.red); 
//                }
            }
        });
        
        _enableControl = true;
    }

    public override Point GetNextPoint(Point startPoint)
    {
        var exits = _connections.Where(point => point != startPoint).ToList();
        if (exits.Count == 0)
        {
            Debug.LogError("Нет выходов у точки");
            return null;
        }

        Point nextPoint = null;
        switch (_direction)
        {
            case Direction.Up:
                nextPoint = exits.FirstOrDefault(point => PositionPredicate(point, false, true));
                break;
            case Direction.Down:
                nextPoint = exits.FirstOrDefault(point => PositionPredicate(point, false, false));
                break;
            case Direction.Left:
                nextPoint = exits.FirstOrDefault(point => PositionPredicate(point, true, false));
                break;
            case Direction.Right:
                nextPoint = exits.FirstOrDefault(point => PositionPredicate(point, true, true));
                break;
        }

        if (nextPoint == null)
        {
            nextPoint = exits.FirstOrDefault(point =>
            {
                var targetPosition = point.transform.position;
                var fromPosition = startPoint.transform.position;
                return Math.Abs(targetPosition.x - fromPosition.x) < TOLERANCE ||
                       Math.Abs(targetPosition.y - fromPosition.y) < TOLERANCE;
            });
        }

        return nextPoint;
    }

    private bool PositionPredicate(Component point, bool horizontal, bool max)
    {
        var targetPosition = point.transform.position;
        var fromPosition = transform.position;
        if (horizontal)
        {
            return max ? targetPosition.x > fromPosition.x : targetPosition.x < fromPosition.x;
        }

        return max ? targetPosition.y > fromPosition.y : targetPosition.y < fromPosition.y;
    }

    public override void AfterApply(Player player)
    {
        base.AfterApply(player);
        _direction = Direction.None;
    }

    private void Update()
    {
        if (_enableControl)
        {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && up.isActiveAndEnabled)
            {
                ClearColors();
                up.SetColor(Color.red);
                _direction = Direction.Up;
            }
            else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && right.isActiveAndEnabled)
            {
                ClearColors();
                right.SetColor(Color.red);
                _direction = Direction.Right;
            }
            else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && down.isActiveAndEnabled)
            {
                ClearColors();
                down.SetColor(Color.red);
                _direction = Direction.Down;
            }
            else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && left.isActiveAndEnabled)
            {
                ClearColors();
                left.SetColor(Color.red);
                _direction = Direction.Left;
            }
        }
    }

    private void ClearColors()
    {
        down.SetColor(Color.white);
        right.SetColor(Color.white);
        left.SetColor(Color.white);
        up.SetColor(Color.white);
    }
}