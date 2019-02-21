using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NeatLine))]
public class NeatLineEditor : Editor
{
    NeatLine line;
    
    private void OnEnable()
    {
        line = target as NeatLine;
    }

    public override void OnInspectorGUI()
    {
        Undo.RecordObject(target, "NeatLine Edit");
        serializedObject.Update();

        DrawDefaultInspector();

        GUI.Box(EditorGUILayout.BeginVertical(), "");
        EditorGUIUtility.wideMode = true;
        line.HeadLocalPosition = EditorGUILayout.Vector2Field("Head Position", line.HeadLocalPosition);
        EditorGUILayout.EndVertical();

        GUI.Box(EditorGUILayout.BeginVertical(), "");
        EditorGUIUtility.wideMode = true;
        line.TailLocalPosition = EditorGUILayout.Vector2Field("Tail Position", line.TailLocalPosition);
        EditorGUILayout.EndVertical();

        GUI.Box(EditorGUILayout.BeginVertical(), "");
        EditorGUIUtility.wideMode = true;
        line.Color = EditorGUILayout.ColorField("Color", line.Color);
        EditorGUILayout.EndVertical();

        GUI.Box(EditorGUILayout.BeginVertical(), "");
        EditorGUIUtility.wideMode = true;
        line.Thickness = EditorGUILayout.FloatField("Thickness", line.Thickness);
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);
    }
}
