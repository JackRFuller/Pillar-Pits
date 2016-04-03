using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hookshot : MonoBehaviour {
    
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

    [Header("Hookshot Bullet")]
    [SerializeField] private GameObject hookshotBullet;
    [SerializeField] private int numberOfHBToSpawn;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform gunEnd;
    private List<GameObject> hookShotBullets;
    private Rigidbody hookShotRB;
    private Vector3 bulletDirection;
    private GameObject currentBullet;
    private bool isBulletMoving;
    private bool bulletActive;

    [Header("Animations")]
    [SerializeField] private Animator hookShotAnimation;

    void Start()
    {
        Init();
    }

    void Init()
    {
        ResetManager.ResetLevel += ResetHookshot;
        ResetManager.ResetLevel += DeActivateAllHookShotBullets;
        cc = GetComponent<CharacterController>();

        PoolHookShotBullets();
    }

    void PoolHookShotBullets()
    {
        hookShotBullets = new List<GameObject>();

        for(int i = 0; i < numberOfHBToSpawn; i++)
        {
            GameObject _hookShotBullet = (GameObject)Instantiate(hookshotBullet, hookshotBullet.transform.position, transform.rotation);
            _hookShotBullet.SetActive(false);
            hookShotBullets.Add(_hookShotBullet);
        }
    }
   

    void Update()
    {
       
        HookShotInput();

        if (isMoving)
            MoveTowardsTarget();

        if (isBulletMoving)
            MoveBullet();

        if (runTimer)
            HookshotCooldown();
    }

    void HookShotInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!bulletActive)
            {
                if (numberOfShotsLeft > 0)
                {
                    StartCoroutine(ShootHookShot());
                }
            }
            else
            {
                CancelHookShot();
            }
           
        }        
        //if (Input.GetMouseButtonUp(1))
        //{
           
        //}
    }

    void CancelHookShot()
    {
        isBulletMoving = false;
        if (currentBullet != null)
            currentBullet.SetActive(false);
        isMoving = false;
        isBulletMoving = false;
        ResetHookShotBullet();
        bulletActive = false;
        Debug.Log("Hit");
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

    void MoveTowardsTarget()
    {
        Vector3 _offset = target - transform.position;
        float speed = hookshotSpeed;
        if (_offset.magnitude > .5f)
        {
            _offset = _offset.normalized * speed;
            cc.Move(_offset * Time.deltaTime);
            float dist = Vector3.Distance(target, transform.position);
            if (dist < 3f)
            {
                isMoving = false;
                CancelHookShot();
                ResetHookShotBullet();
            }
        }
    }

    IEnumerator ShootHookShot()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "LevelGeo")
            {
                //Spawn In Hookshot Bullet
                for(int i = 0; i < hookShotBullets.Count; i++)
                {
                    if (!hookShotBullets[i].activeInHierarchy)
                    {
                        yield return StartCoroutine(WaitAndPlayShootingAnimation());
                        currentBullet = hookShotBullets[i];
                         hookShotRB = currentBullet.GetComponent<Rigidbody>();
                        if (hookShotRB.isKinematic)
                            hookShotRB.isKinematic = false;
                        currentBullet.transform.parent = gunEnd.transform;
                        currentBullet.transform.localPosition = Vector3.zero;
                        currentBullet.transform.parent = null;
                        currentBullet.SetActive(true);
                        bulletActive = true;
                        break;
                    }
                }

                //Update Hookshot UI
                numberOfShotsLeft--;
                runTimer = true;
                cooldownTime = 0;
                LevelUIManager.instance.TurnOffHookshotBars(numberOfShotsLeft);

                target = hit.point;
                bulletDirection = target - currentBullet.transform.position;
                isBulletMoving = true;

                StartCoroutine(WaitAndPlayHolster());
            }
            else
            {
                Debug.Log("DERP!");
            }
        }
    }

    IEnumerator WaitAndPlayShootingAnimation()
    {
        SetAnimationState("isShooting");
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator WaitAndPlayHolster()
    {
        SetAnimationState("isHolstering");
        yield return new WaitForSeconds(0.3f);
        SetAnimationState("isIdle");
    }



    void MoveBullet()
    {
        hookShotRB.AddRelativeForce(bulletDirection * bulletSpeed, ForceMode.Force);

        float _dist = Vector3.Distance(target, currentBullet.transform.position);

        if(_dist < 3)
        {            
            currentBullet.transform.position = target;
            hookShotRB.isKinematic = true;
            hookShotRB.velocity = Vector3.zero;
            isBulletMoving = false;
            isMoving = true;
        }
    }

    void ResetHookShotBullet()
    {
        bulletDirection = Vector3.zero;
        hookShotRB.velocity = Vector3.zero;
        if(currentBullet != null)
            currentBullet.SetActive(false);
        currentBullet = null;
    }

    void ResetHookshot()
    {
        isBulletMoving = false;
        isMoving = false;
        numberOfShotsLeft = 2;
        LevelUIManager.instance.TurnOnHookshotBars(numberOfShotsLeft);
    }

    void SetAnimation(string _animState)
    {
        hookShotAnimation.SetBool(_animState, true);

        for (int i = 0; i < hookShotAnimation.parameterCount; i++)
        {
            string _parameterName = hookShotAnimation.parameters[i].name;

            if (_parameterName != _animState)
                hookShotAnimation.SetBool(_parameterName, false);
        }
    }

    void DeActivateAllHookShotBullets()
    {
        for(int i = 0; i < hookShotBullets.Count; i++)
        {
            hookShotBullets[i].SetActive(false);
        }

        currentBullet = null;
    }

    public void SetAnimationState(string _animParameter)
    {
        hookShotAnimation.SetBool(_animParameter, true);

        for (int i = 0; i < hookShotAnimation.parameterCount; i++)
        {
            string _parameterName = hookShotAnimation.parameters[i].name;

            if (_parameterName != _animParameter)
                hookShotAnimation.SetBool(_parameterName, false);
        }
    }
}
