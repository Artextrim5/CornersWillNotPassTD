using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(WayPoint))]
public class WaypointEditor : Editor
{
    WayPoint WayPoint => target as WayPoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.red;
        for (int i = 0; i< WayPoint.Points.Length; i ++)
        {
            EditorGUI.BeginChangeCheck();

            // Create Handels
            Vector3 curentWaypointPoint = WayPoint.CurentPosition + WayPoint.Points[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(curentWaypointPoint, Quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

            // Create text
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.yellow;
            Vector3 textAlligment = Vector3.down * 0.35f + Vector3.right * 0.35f;
            Handles.Label(WayPoint.CurentPosition + WayPoint.Points[i] + textAlligment, $"{i+1}", textStyle);

            EditorGUI.EndChangeCheck();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move Handle");
                WayPoint.Points[i] = newWaypointPoint - WayPoint.CurentPosition;
            }
        }
    }
}
