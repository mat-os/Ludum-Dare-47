using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Transistor : Point
{
    public float slowKoef = 1;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    private Direction _direction = Direction.None;
    private readonly Dictionary<Point, GameObject> _arrows = new Dictionary<Point, GameObject>();

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
        _player.Velocity /= slowKoef;
    }

    public override void Apply(Player player)
    {
        base.Apply(player);
        player.Velocity *= slowKoef;
        foreach (var arrow in _arrows.Values)
        {
            arrow.SetActive(false);
        }
    }

    public override void SetAsNext(Player player)
    {
        var exits = _connections.Where(point => point != player.StartPoint).ToList();
        exits.ForEach(point =>
        {
            if (_arrows.ContainsKey(point))
            {
                _arrows[point].SetActive(true);
            }
        });
    }

    protected override Point GetNextPoint(Point startPoint)
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

        return nextPoint == null ? base.GetNextPoint(startPoint) : nextPoint;
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
        if (_player != null)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                _direction = Direction.Up;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _direction = Direction.Right;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                _direction = Direction.Down;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _direction = Direction.Left;
            }
        }
    }

    private enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}