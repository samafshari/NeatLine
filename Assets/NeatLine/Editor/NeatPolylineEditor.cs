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

        line.ThicknessMultiplier = EditorGUILayout.FloatField($"Thickness Multiplier", line.ThicknessMultiplier);

        int i = -1;
        foreach (var point in line._points)
        {
            i++;
            GUI.Box(EditorGUILayout.BeginVertical(), "");
            EditorGUIUtility.wideMode = true;
            line._points[i] = EditorGUILayout.Vector2Field($"Point {i}", line._points[i]);
            line._colors[i] = EditorGUILayout.ColorField($"Color {i}", line._colors[i]);
            line._thicknesses[i] = EditorGUILayout.FloatField($"Thickness {i}", line._thicknesses[i]);
            if (GUILayout.Button($"Remove Point {i}")) line.RemoveAt(i);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();
        }

        GUI.Box(EditorGUILayout.BeginVertical(), "");

        if (GUILayout.Button("Add Point")) line.Add();
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);

        line.MakeDirty();
    }
}
