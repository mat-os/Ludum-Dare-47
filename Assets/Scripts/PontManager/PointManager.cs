using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviourSingleton<PointManager>
{
    public GameObject PointHolder;

    [Header("Prefabs")]
    public GameObject PointPrefab;
    public GameObject TransistorPrefab;
    public GameObject ResistorPrefab;
    public GameObject CondensatorPrefab;
    

    public List<Point> _points = new List<Point>();
    private void Awake()
    {
        InitLevelPoints();
    }

    public void InitLevelPoints()
    {
        foreach (Transform point in PointHolder.transform)
        {
            if (point.TryGetComponent(out Point p))
            {
                _points.Add(p);
            }
        }
    }
}
