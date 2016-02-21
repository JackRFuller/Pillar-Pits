using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance = null;

    [Header("Level Attributes")]
    public int currentTargets; //Identifies the number of targets for the level
    private float levelTimer = 0; //The level timer
    private bool isLevelTimerRunning = true; //Determines if the level timer is running

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {

        Init();
	
	}

    void Init()
    {
        levelTimer = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if(isLevelTimerRunning)
            RunTimer();
	
	}

    void RunTimer()
    {
        levelTimer += Time.deltaTime;
        LevelUIManager.instance.UpdateTimer(levelTimer);
    }

    //Reduce Number of Targets by 1
    public void DecrementTarget()
    {
        currentTargets--;
        LevelUIManager.instance.TurnOffTargets(currentTargets);

        if (currentTargets == 0)
            isLevelTimerRunning = false;
    }
}
