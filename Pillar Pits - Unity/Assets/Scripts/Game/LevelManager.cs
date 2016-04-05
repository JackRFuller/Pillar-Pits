using UnityEngine;
using System.Collections;
using LitJson;

[System.Serializable]
public class LevelAttributes
{
    public int levelID;
    [HideInInspector] public int startingNumberOfTargets;
    [HideInInspector] public int startingClipAmmo;
    [HideInInspector] public int totalStartingAmmo;
    [HideInInspector] public int[] starTimes = new int[3];
    [HideInInspector] public Vector3 startingPlayerPos;
    [HideInInspector] public Vector3 startingPlayerRot;
}

public class LevelManager : MonoBehaviour    
{

    public enum levelType
    {
        levelBuilding,
        levelTesting,
    }

    public levelType currentLevelType;
   
    public static LevelManager instance = null;
    public LevelAttributes levelAttributes;
    public delegate void Initalise();
    public static Initalise InitaliseLevel;
    public bool debugMode;

    private int currentTargets; //Identifies the number of targets still active in the level
    private float levelTimer = 0; //The level timer
    private bool isLevelTimerRunning = true; //Determines if the level timer is running

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    void AssignValues(JsonData[] dataObjects)
    {
        for(int i = 0; i < dataObjects.Length; i++)
        {
            if (dataObjects[i].Keys.Contains("levelID"))
            {
                if(levelAttributes.levelID == (int)dataObjects[i]["levelID"])
                {
                    string _path = "Prefabs/Levels/";                    

                    if (dataObjects[i].Keys.Contains("numberOfTargets"))
                        levelAttributes.startingNumberOfTargets = (int)dataObjects[i]["numberOfTargets"];

                    Debug.Log("Number of Targets" + levelAttributes.startingNumberOfTargets);

                    if (dataObjects[i].Keys.Contains("startingClipAmmo"))
                        levelAttributes.startingClipAmmo = (int)dataObjects[i]["startingClipAmmo"];

                    Debug.Log("Starting Clip Ammo" + levelAttributes.startingClipAmmo);

                    if (dataObjects[i].Keys.Contains("totalStartingAmmo"))
                        levelAttributes.totalStartingAmmo = (int)dataObjects[i]["totalStartingAmmo"];

                    Debug.Log("Starting Ammo" + levelAttributes.totalStartingAmmo);

                    if (currentLevelType == levelType.levelTesting)
                    {
                        if (UnityDataConnector.currentDataSheet == UnityDataConnector.dataSheet.Jack)
                        {
                            _path += "Jack/";
                        }

                        if (UnityDataConnector.currentDataSheet == UnityDataConnector.dataSheet.Chris)
                        {
                            _path += "Chris/";
                        }

                        //Load In Level
                        string _levelName = "Level " + levelAttributes.levelID.ToString();

                        _path += _levelName;

                        Debug.Log(_path);

                        GameObject _level = Instantiate(Resources.Load(_path, typeof(GameObject))) as GameObject;

                        Debug.Log("Level ID: " + (int)dataObjects[i]["levelID"]);

                        //Player Position
                        if (dataObjects[i].Keys.Contains("playerStartingPosX"))
                            levelAttributes.startingPlayerPos.x = (int)dataObjects[i]["playerStartingPosX"];
                        if (dataObjects[i].Keys.Contains("playerStartingPosY"))
                            levelAttributes.startingPlayerPos.y = (int)dataObjects[i]["playerStartingPosY"];
                        if (dataObjects[i].Keys.Contains("playerStartingPosZ"))
                            levelAttributes.startingPlayerPos.z = (int)dataObjects[i]["playerStartingPosZ"];

                        //Player Rotation
                        if (dataObjects[i].Keys.Contains("playerStartingRotX"))
                            levelAttributes.startingPlayerRot.x = (int)dataObjects[i]["playerStartingRotX"];
                        if (dataObjects[i].Keys.Contains("playerStartingRotY"))
                            levelAttributes.startingPlayerRot.y = (int)dataObjects[i]["playerStartingRotY"];
                        if (dataObjects[i].Keys.Contains("playerStartingRotZ"))
                            levelAttributes.startingPlayerRot.z = (int)dataObjects[i]["playerStartingRotZ"];

                        //Star Times
                        if (dataObjects[i].Keys.Contains("star1Time"))
                            levelAttributes.starTimes[0] = (int)dataObjects[i]["star1Time"];
                        if (dataObjects[i].Keys.Contains("star2Time"))
                            levelAttributes.starTimes[1] = (int)dataObjects[i]["star2Time"];
                        if (dataObjects[i].Keys.Contains("star3Time"))
                            levelAttributes.starTimes[2] = (int)dataObjects[i]["star3Time"]; 
                    }

                    break;
                }
            }
        }

        Init();
    }    

    //Used for initialising and reseting a level
    void Init()
    {
        ResetManager.ResetLevel += Init;
        currentTargets = levelAttributes.startingNumberOfTargets;
        levelTimer = 0;       

        //Gameplay Initilise - Called by PC
        InitaliseLevel();

        //UI Elements
        LevelUIManager.instance.TurnOnTargets(levelAttributes.startingNumberOfTargets);
        LevelUIManager.instance.TurnOffLoadScreen();
        isLevelTimerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLevelTimerRunning)
            RunTimer();

        if (Input.GetKeyUp(KeyCode.Tab))
            ResetManager.ResetLevel();

    }

    void RunTimer()
    {
        levelTimer += Time.smoothDeltaTime * Time.timeScale;
        LevelUIManager.instance.UpdateTimer(levelTimer);
    }

    //Reduce Number of Targets by 1
    public void DecrementTarget()
    {
        currentTargets--;
        LevelUIManager.instance.TurnOffTargets(currentTargets);

        if (currentTargets == 0)
            EndLevel();
           
    }

    void EndLevel()
    {
        LevelUIManager.instance.SetupLevelOverUI(levelTimer);
        isLevelTimerRunning = false;
    }
}
