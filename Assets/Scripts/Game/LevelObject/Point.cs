using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Point : MonoBehaviour
{
    protected static readonly float TOLERANCE = 0.01f;
    public List<Point> _connections = new List<Point>();
    protected Player _player;

    [Header("Debug")]
    public float OnGizmoLineWidth;

    public virtual void Apply(Player player)
    {
        Debug.Log($"Apply {gameObject.name}");
        var playerStartPoint = player.PrevPoint;
        player.PrevPoint = this;
        player.NextPoint = GetNextPoint(playerStartPoint);
        _player = null;
    }

    public virtual void SetAsNext(Player player)
    {
        
    }

    public virtual void BeforeApply(Player player)
    {
        Debug.Log($"Before Apply {gameObject.name}");
        _player = player;
    }

    public virtual void AfterApply(Player player)
    {
        Debug.Log($"After Apply {gameObject.name}");
    }

    public virtual Point GetNextPoint(Point startPoint)
    {
        var exits = _connections.Where(point => point != startPoint).ToList();
        if (exits.Count == 0)
        {
            Debug.LogError("Нет выходов у точки");
            return null;
        }

        var nextPoint = exits.FirstOrDefault(point =>
        {
            var targetPosition = point.transform.position;
            var fromPosition = startPoint.transform.position;
            return Math.Abs(targetPosition.x - fromPosition.x) < TOLERANCE ||
                   Math.Abs(targetPosition.y - fromPosition.y) < TOLERANCE;
        });

        if (nextPoint == null)
        {
            nextPoint = exits[0];
        }

        return nextPoint;
    }

    private void OnDrawGizmos()
    {
        if (_player != null)
        {
            _connections.ForEach(point =>
            {
                if (point == null)
                {
                    Debug.LogWarning($"У точки {gameObject.name} заданы пустые связи");
                    return;
                }
                
                DrawLine(transform.position, point.transform.position, OnGizmoLineWidth,
                    point == _player.NextPoint ? Color.red : Color.blue);
            });
        }
        else
        {
            _connections.ForEach(point =>
            {
                if (point == null)
                {
                    Debug.LogWarning($"У точки {gameObject.name} заданы пустые связи");
                    return;
                }
                DrawLine(transform.position, point.transform.position, 0.1f, Color.white);
            });
        }
    }

    private void DrawLine(Vector3 p1, Vector3 p2, float width, Color color)
    {
        var count = 1 + Mathf.CeilToInt(width); // how many lines are needed.
        if (count == 1)
        {
            Gizmos.DrawLine(p1, p2);
        }
        else
        {
            var c = Camera.current;
            if (c == null)
            {
                Debug.LogError("Camera.current is null");
                return;
            }

            var scp1 = c.WorldToScreenPoint(p1);
            var scp2 = c.WorldToScreenPoint(p2);

            var v1 = (scp2 - scp1).normalized; // line direction
            var n = Vector3.Cross(v1, Vector3.forward); // normal vector

            for (var i = 0; i < count; i++)
            {
                var o = 0.99f * n * width * ((float) i / (count - 1) - 0.5f);
                var origin = c.ScreenToWorldPoint(scp1 + o);
                var destiny = c.ScreenToWorldPoint(scp2 + o);

                Gizmos.color = color;
                Gizmos.DrawLine(origin, destiny);
            }
        }
    }
}