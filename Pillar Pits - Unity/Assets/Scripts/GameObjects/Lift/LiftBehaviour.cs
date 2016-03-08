using UnityEngine;
using System.Collections;

public class LiftBehaviour : MonoBehaviour
{
    public enum mode
    {
        Once,
        PingPong,
    }

    public mode Mode;

    [Header("Destinations")]
    public Vector3 startingPos;
    public Vector3 endingPos;

    [Header("Attributes")]
    [SerializeField] private float duration;

    //Lerping Variables
    private bool isMoving;
    private Vector3 startPos;
    private Vector3 endPos;
    private float timeStartedLerping;
    private int movementCount = 0;

    void Start()
    {
        Init();
    }

    void Init()
    {
        ResetManager.ResetLevel += Init;
        transform.position = startingPos;
        isMoving = false;
    }

    void Activate()
    {
        timeStartedLerping = Time.time;

        if(movementCount == 0)
        {
            startPos = startingPos;
            endPos = endingPos;
        }
        else
        {
            startPos = endingPos;
            endPos = startingPos;          
        }

        isMoving = true;        
    }

    void Update()
    {
        if (isMoving)
            MoveToPosition();
    }

    void MoveToPosition()
    {
        float _timeSinceStarted = Time.time - timeStartedLerping;
        float _percentageComplete = _timeSinceStarted / duration;

        transform.position = Vector3.Lerp(startPos, endPos, _percentageComplete);

        if(_percentageComplete >= 1.0F)
        {
            if(Mode == mode.PingPong)
            {
                if (movementCount == 1)
                    movementCount = 0;
                else movementCount++;
                Activate();
            }
            else
            {
                isMoving = false;
            }
           
        }
    }
	
    public void SetStartPos()
    {
        startingPos = transform.position;
    }

    public void SetEndPos()
    {
        endingPos = transform.position;
    }

    public void MoveToStartPosition()
    {
        transform.position = startingPos;
    }
}
