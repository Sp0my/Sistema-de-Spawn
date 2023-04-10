using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Spawnables))]
public class SpawnableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Spawnables spawnables = (Spawnables)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Recycle"))
        {
            spawnables.Recycle();
        }
    }
}
