using System.Collections.Generic;
using UnityEngine;

namespace Script.Utils
{
    public static class Utils
    {
        public static void ClearGameObjectFromChildes(Transform gameObjectToClear)
        {
            foreach (Transform child in gameObjectToClear.transform) {
                Object.Destroy(child.gameObject);
            }
        }
        public static void SetActiveAllChildren(Transform transform, bool value)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(value);
 
                SetActiveAllChildren(child, value);
            }
        }
        public static void SetActiveArray(GameObject[] array, bool activeState)
        {
            foreach (var o in array)
            {
                o.SetActive(activeState);
            }
        }
        
        public static void SetLayerRecursively(this GameObject obj, int layer) {
            obj.layer = layer;
 
            foreach (Transform child in obj.transform) {
                child.gameObject.SetLayerRecursively(layer);
            }
        }
        
        public static void Shuffle<T>(this T[] theArr) 
        {
            for (int i = 0; i < theArr.Length; i++) {
                int rnd = Random.Range(0, theArr.Length);
                var tempGO = theArr[rnd];
                theArr[rnd] = theArr[i];
                theArr[i] = tempGO;
            }
        }
    }
}