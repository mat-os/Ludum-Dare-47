using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    public GameObject LineRendererPrefab;
    
    public GameObject[] ConnectedPointArray;

    private LineRenderer[] _lineRenderers;

    private void Start()
    {
        SpawnLineRenderers();

        DrawLines();
    }

    private void SpawnLineRenderers()
    {
        _lineRenderers = new LineRenderer[ConnectedPointArray.Length];

        for (var i = 0; i < ConnectedPointArray.Length; i++)
        {
            var newLr = Instantiate(LineRendererPrefab, gameObject.transform);
            
            _lineRenderers[i] = newLr.GetComponent<LineRenderer>();
        }
    }
    public void DrawLines()
    {
        for (var i = 0; i < _lineRenderers.Length; i++)
        {
            var line = _lineRenderers[i];
            
            line.SetPosition(0, transform.position);
            line.SetPosition(1, ConnectedPointArray[i].transform.position);
        }
    }
}
