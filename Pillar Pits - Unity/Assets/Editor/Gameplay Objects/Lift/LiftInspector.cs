using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LiftBehaviour))]
public class LiftInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LiftBehaviour thisLift = (LiftBehaviour)target;

        if (GUILayout.Button("Set Starting Position"))
            thisLift.SetStartPos();

        if (GUILayout.Button("Set End Position"))
            thisLift.SetEndPos();

        if (GUILayout.Button("Move To Start Position"))
            thisLift.MoveToStartPosition();
    }

}
