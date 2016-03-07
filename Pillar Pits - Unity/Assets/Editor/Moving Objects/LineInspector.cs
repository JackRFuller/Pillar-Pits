using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectLine))]
public class LineInspector : Editor
{
    private void OnSceneGUI()
    {
        ObjectLine _line = target as ObjectLine;
        Transform handleTransform =_line.transform;
        Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ? 
            handleTransform.rotation : Quaternion.identity;

        Vector3 point0 = handleTransform.TransformPoint(_line.pointA);
        Vector3 point1 = handleTransform.TransformPoint(_line.pointB);

        Handles.color = Color.white;
        Handles.DrawLine(point0, point1);
        Handles.DoPositionHandle(point0, handleRotation);
        Handles.DoPositionHandle(point1, handleRotation);

        EditorGUI.BeginChangeCheck();
        point0 = Handles.DoPositionHandle(point0, handleRotation);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_line, "Move Point");
            EditorUtility.SetDirty(_line);
            _line.pointA = handleTransform.InverseTransformPoint(point0);
        }
            

        EditorGUI.BeginChangeCheck();
        point1 = Handles.DoPositionHandle(point1, handleRotation);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_line, "Move Point");
            EditorUtility.SetDirty(_line);
            _line.pointB = handleTransform.InverseTransformPoint(point1);
        }
            


    }
	
}
