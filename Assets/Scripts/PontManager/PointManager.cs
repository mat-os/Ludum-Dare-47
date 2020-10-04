using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviourSingleton<PointManager>
{
    public GameObject[] PointHolders;

    [Header("Prefabs")]
    public GameObject PointPrefab;
    public GameObject TransistorPrefab;
    public GameObject ResistorPrefab;
    public GameObject CondensatorPrefab;
    

   [HideInInspector] public List<Point> _points = new List<Point>();
    private void Awake()
    {
        InitLevelPoints();
    }

    public void InitLevelPoints()
    {
        foreach (var holder in PointHolders)
        {
            foreach (Transform point in holder.transform)
            {
                if (point.TryGetComponent(out Point p))
                {
                    _points.Add(p);
                }
            }
        }
    }
}
