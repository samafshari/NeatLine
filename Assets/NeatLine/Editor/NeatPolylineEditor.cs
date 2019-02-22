using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NeatPolyline))]
public class NeatPolylineEditor : Editor
{
    NeatPolyline line;

    private void OnEnable()
    {
        line = target as NeatPolyline;
    }
    
    public override void OnInspectorGUI()
    {
        Undo.RecordObject(target, "NeatLine Edit");
        serializedObject.Update();

        //DrawDefaultInspector();
        GUI.Box(EditorGUILayout.BeginVertical(), "");
        EditorGUIUtility.wideMode = true;
        line.UpdateInEditMode = EditorGUILayout.Toggle($"Update in Edit Mode", line.UpdateInEditMode);
        EditorGUILayout.EndVertical();

        int i = -1;
        foreach (var point in line._points)
        {
            i++;
            GUI.Box(EditorGUILayout.BeginVertical(), "");
            EditorGUIUtility.wideMode = true;
            line._points[i] = EditorGUILayout.Vector2Field($"Point {i}", line._points[i]);
            EditorGUILayout.EndVertical();

            GUI.Box(EditorGUILayout.BeginVertical(), "");
            EditorGUIUtility.wideMode = true;
            line._colors[i] = EditorGUILayout.ColorField($"Color {i}", line._colors[i]);
            EditorGUILayout.EndVertical();

            GUI.Box(EditorGUILayout.BeginVertical(), "");
            EditorGUIUtility.wideMode = true;
            line._thicknesses[i] = EditorGUILayout.FloatField($"Thickness {i}", line._thicknesses[i]);
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);
    }
}
