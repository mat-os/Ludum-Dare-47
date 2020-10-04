using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PointRedactor))]
public class PointRedactorEditor : Editor
{
    PointRedactor myTarget;
    private Point thisPoint;

    public override void OnInspectorGUI()
    {
        myTarget = (PointRedactor)target;
        DrawDefaultInspector();
        thisPoint = myTarget.GetComponent<Point>();
        GameObject newPointObject = null;
        //Линия
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
        GUILayout.BeginVertical();

        if (GUILayout.Button("Add Point"))
        {
            newPointObject = SpawnObject(PointManager.Instance.PointPrefab);
            AddConnections(newPointObject);
        }
            
        if (GUILayout.Button("Add Transistor"))
        {
             newPointObject = SpawnObject(PointManager.Instance.TransistorPrefab);
             AddConnections(newPointObject);

        }
         
        if (GUILayout.Button("Add Resistor"))
        {
             newPointObject = SpawnObject(PointManager.Instance.ResistorPrefab);
             AddConnections(newPointObject);

        }
        if (GUILayout.Button("Add Condensator"))
        {
             newPointObject = SpawnObject(PointManager.Instance.CondensatorPrefab);
             AddConnections(newPointObject);
        }
        
        GUILayout.EndVertical();

    }

    private void AddConnections(GameObject newPointObject)
    {
        var newPoint = newPointObject.GetComponent<Point>();
        newPoint._connections.Add(thisPoint);
        thisPoint._connections.Add(newPoint);
    }

    private GameObject SpawnObject(GameObject gameObject)
    {
        var posToSpawn = myTarget.transform.position + Vector3.up * 1;
        var newPointObject = PrefabUtility.InstantiatePrefab(gameObject, PointManager.Instance.PointHolders[0].transform) as GameObject;
        newPointObject.transform.position = posToSpawn;
        return newPointObject;
    }
}
