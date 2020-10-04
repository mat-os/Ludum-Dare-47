using System;
using System.Collections.Generic;
using UnityEngine;


    public class LineRendererManager : MonoBehaviour
    {
        public GameObject LineRendererPrefab;
        private List<LineRenderer> _lineRenderers = new List<LineRenderer>();

        private List<Point> _points = new List<Point>();

        private void Start()
        {
            _points =  PointManager.Instance._points;
            
            InitLines();
        }

        private void InitLines()
        {
            foreach (var point in _points)
            {
                SpawnLineRenderers(point);
            }
        }
        
        private void SpawnLineRenderers(Point point)
        {
            for (var i = 0; i < point._connections.Count; i++)
            {
                if (point._connections != null)
                {
                    var newLr = Instantiate(LineRendererPrefab, PointManager.Instance.PointHolders[0].transform).GetComponent<LineRenderer>();

                    newLr.SetPosition(0, point.transform.position);
                    newLr.SetPosition(1, point._connections[i].transform.position);
                
                    _lineRenderers.Add(newLr.GetComponent<LineRenderer>());
                }
            }
        }
    }
