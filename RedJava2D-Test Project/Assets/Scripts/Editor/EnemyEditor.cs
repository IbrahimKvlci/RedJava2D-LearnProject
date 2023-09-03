using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


#if UNITY_EDITOR
[CustomEditor(typeof(EnemyControl))]
[System.Serializable]
class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnemyControl script = (EnemyControl)target;
        if (GUILayout.Button("Create", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject newObject = new GameObject();
            newObject.transform.parent = script.transform;
            newObject.transform.position = script.transform.position;
            newObject.name = script.transform.childCount.ToString();
        }
    }
}
#endif
