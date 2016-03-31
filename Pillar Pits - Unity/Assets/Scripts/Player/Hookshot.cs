using UnityEngine;
using System.Collections;

public class Hookshot : MonoBehaviour {

    
    [Header("DoubleClickAttributes")]
    [SerializeField] private float timeForDoubleClickCheck;
    private bool oneClick;
    private float doubleClickTimer;
    private bool hasDoubleClicked;

    [Header("Hookshot Attributes")]
    [SerializeField] private float hookshotSpeed;
    [SerializeField] private float hookshotModifier;   
    [SerializeField] private float timeTakenToExtendHookshot;
    private Vector3 target;

    //Lerping Attributes
    private bool isLerping;
    private float timeStartedLerping;
    private Vector3 startPos;
    private Vector3 endPos;

    //Character Attributes
    private CharacterController cc;
    public LineRenderer lr;
    private bool isMoving;

    [Header("Cooldown Attributes")]
    [SerializeField] private float hookshotCooldownTime;
    private int numberOfShotsLeft = 2;
    private float cooldownTime;
    private bool runTimer;

    [Header("Particle Effects")]
    [SerializeField] private Transform gunEnd;
    [SerializeField] private GameObject initialShot;   
    [SerializeField] private GameObject wave;
    [SerializeField] private float disturbance;
    private GameObject currentShot;
    private bool hookshotOut;

    void Start()
    {
        ResetManager.ResetLevel += ResetHookshot;
        cc = GetComponent<CharacterController>();
       
    }
   

    void Update()
    {
       
        HookShotInput();

        if (isMoving)
            MoveTowardsTarget();

        if (isLerping)
            LerpHookShot();

        if (runTimer)
            HookshotCooldown();
    }

    void HookshotCooldown()
    {
        cooldownTime += Time.deltaTime;
        if(cooldownTime >= hookshotCooldownTime)
        {
            numberOfShotsLeft++;
            cooldownTime = 0;
            LevelUIManager.instance.TurnOnHookshotBars(numberOfShotsLeft);
            if(numberOfShotsLeft == 2)
            {
                runTimer = false;
            }
        }
    }

    void LerpHookShot()
    {
        float _timeSinceStarted = Time.time - timeStartedLerping;
        float _percentageComplete = _timeSinceStarted / timeTakenToExtendHookshot;
        Vector3 _hookshotLine = Vector3.Lerp(startPos, endPos, _percentageComplete);
        lr.SetPosition(1, _hookshotLine);

        if(_percentageComplete >= 1.0f)
        {
            isLerping = false;
            isMoving = true;
        }

    }

    void MoveTowardsTarget()
    {
        Vector3 _offset = target - transform.position;
        float speed = hookshotSpeed;
        if (_offset.magnitude > .5f)
        {
            if (hasDoubleClicked)
            speed = hookshotSpeed * hookshotModifier;
            _offset = _offset.normalized * speed;
            cc.Move(_offset * Time.deltaTime);
            float dist = Vector3.Distance(target, transform.position);
            if (dist < 1f)
            {
                isMoving = false;
                if (currentShot != null)
                    currentShot.GetComponent<BeamParam>().bEnd = true;
            }
                

            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, target);
        }
    }

    void HookShotInput()
    {
        if (Input.GetMouseButtonDown(1))
        {           
            if(numberOfShotsLeft > 0)
            {
                oneClick = true;
                doubleClickTimer = Time.time;
                ShootHookShot(false);
                SendOutParticleEffect();
                
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (hookshotOut)
            {
                currentShot.transform.position = gunEnd.position;

                BeamParam bp = GetComponent<BeamParam>();
                if (currentShot.GetComponent<BeamParam>().bGero)
                    currentShot.transform.parent = transform;

                Vector3 s = new Vector3(bp.Scale, bp.Scale, bp.Scale);

                currentShot.transform.localScale = s;
                currentShot.GetComponent<BeamParam>().SetBeamParam(bp);
            }
        }   
        if (Input.GetMouseButtonUp(1))
        {
            isMoving = false;
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
            hasDoubleClicked = false;

            if (currentShot != null)
                currentShot.GetComponent<BeamParam>().bEnd = true;

            hookshotOut = false;
        }
    }

    void SendOutParticleEffect()
    {
        GameObject hookShot = (GameObject)Instantiate(wave, gunEnd.position, gunEnd.rotation);
        hookShot.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;

        GameObject hookShotWave;

        hookShotWave = initialShot;
        currentShot = (GameObject)Instantiate(hookShotWave, gunEnd.position, gunEnd.rotation);

        hookshotOut = true;
    }

    void ShootHookShot(bool _doubleClicked)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider)
            {
                //Update Hookshot UI
                numberOfShotsLeft--;
                runTimer = true;
                cooldownTime = 0;
                LevelUIManager.instance.TurnOffHookshotBars(numberOfShotsLeft);


                target = hit.point;
                //isMoving = true;

                if (_doubleClicked)
                    hasDoubleClicked = true;
                else hasDoubleClicked = false;

                SetUpHookshotLerp();
                 
            }
        }
    }

    void SetUpHookshotLerp()
    {
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.zero);
        timeStartedLerping = Time.time;
        startPos = transform.position;
        endPos = target;
        isLerping = true;
    }

    void ResetHookshot()
    {
        isLerping = false;
        isMoving = false;
        numberOfShotsLeft = 2;
        LevelUIManager.instance.TurnOnHookshotBars(numberOfShotsLeft);
    }
}
