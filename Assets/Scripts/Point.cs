using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class Point : MonoBehaviour
{
    private static readonly float TOLERANCE = 0.01f;
    private List<Point> _connections = new List<Point>();

    public virtual void Apply(Player player)
    {
        player.NextPoint = GetNextPoint(player.StartPoint);
    }

    protected Point GetNextPoint(Point startPoint)
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
}