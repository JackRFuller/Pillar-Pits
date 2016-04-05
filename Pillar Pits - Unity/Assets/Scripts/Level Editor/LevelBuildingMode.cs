using UnityEngine;
using System.Collections;

public class LevelBuildingMode : MonoBehaviour
{
    [HideInInspector][SerializeField] private PlayerSetup setupScript;
    [HideInInspector][SerializeField] private LevelManager levelScript;

    private int count = 0;

    public void SetLevelMode()
    {
        if (count == 0)
            Test();
        if (count == 1)
            Build();

        count++;
        if (count > 1)
            count = 0;
    }

	void Test()
    {
        setupScript.buildingLevel = false;
        levelScript.currentLevelType = LevelManager.levelType.levelTesting;
    }

    void Build()
    {
        setupScript.buildingLevel = true;
        levelScript.currentLevelType = LevelManager.levelType.levelBuilding;
    }
}
