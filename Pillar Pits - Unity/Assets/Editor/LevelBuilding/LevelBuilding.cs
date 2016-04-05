using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(LevelBuildingMode))]
public class LevelBuilding : Editor {

    private string buttonName = "Swap";
    private int count =0;

    

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelBuildingMode script = (LevelBuildingMode)target;

        if(GUILayout.Button(buttonName))
        {
            script.SetLevelMode();
            if (count == 0)
                buttonName = "Build";
            if (count == 1)
                buttonName = "Test";

            count++;
            if (count > 1)
                count = 0;
        }
    }
}
