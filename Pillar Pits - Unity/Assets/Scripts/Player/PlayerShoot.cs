using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShoot : ShootingClass {

    [Header("ParticleSystem")]
    public ParticleSystem gunSystem;

    [Header("Bullets")]
    public GameObject bulletPrefab;
    [SerializeField] private int numOfBulletsToSpawn;
    private List<GameObject> pooledBullets;
    public Transform gunMuzzle;

    [Header("Melee")]
    [SerializeField] private float meleeRange;
    [SerializeField] private float meleeCoolDownTime;
    private float meleeCoolDown;


    void Start()
    {
        LevelManager.InitaliseLevel += Init;

        PoolBullets();
    }

    //Initilise the ammount of ammo the user has
    void Init()
    {
        ResetManager.ResetLevel += Init;

        currentClipAmmo = LevelManager.instance.levelAttributes.startingClipAmmo;
        currentTotalAmmo = LevelManager.instance.levelAttributes.totalStartingAmmo;

        LevelUIManager.instance.UpdateTotalAmmo(currentTotalAmmo);
        LevelUIManager.instance.InitialiseNumberOfBullets(currentClipAmmo);
    }

    void PoolBullets()
    {
        pooledBullets = new List<GameObject>();

        for(int i = 0; i < numOfBulletsToSpawn; i++)
        {
            GameObject _bullet = Instantiate(bulletPrefab) as GameObject;
            _bullet.SetActive(false);
            pooledBullets.Add(_bullet);
        }
    }
	
	// Update is called once per frame
	void Update () {

        CheckForTargets();

        if (Input.GetMouseButton(0))
        {
            //Check Ammo & Cooldown Time
            if(AmmoCheck() && CooldownCheck())
            {                
                SendOutRayCast();
            }

            if (!AmmoCheck() && CooldownCheck())
            {
                Reload();
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            //Check that the actor isn't shooting
            if (CooldownCheck())
            {
                Reload();
                Debug.Log("Reload");
            }

                      
        }

        if (Input.GetMouseButtonDown(2))
        {
            if (!isReloading && CooldownCheck())
            {
                if (meleeCoolDown < Time.time)
                    StartCoroutine(Melee());
            }
            
        }
	}

    void CheckForTargets()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit; 
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Target")
            {
                LevelUIManager.instance.TurnReticleRed();
            }
            else
            {
                LevelUIManager.instance.TurnReticleWhite();
            }                
        }
        else
        {
            LevelUIManager.instance.TurnReticleWhite();
        }
    }

     IEnumerator Melee()
    {
        if(weaponAnim)
            SetAnimationState("isHitting");
        yield return new WaitForSeconds(0.3f);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, meleeRange);
        for(int i = 0; i < hitColliders.Length; i++)
        {
            if(hitColliders[i].GetComponent<Renderer>())
                if(hitColliders[i].GetComponent<Renderer>().isVisible)
                    hitColliders[i].gameObject.SendMessage("Hit", 100, SendMessageOptions.DontRequireReceiver);
        }
        yield return new WaitForSeconds(0.6f);
        meleeCoolDown += meleeCoolDownTime;
        if(weaponAnim)
            SetAnimationState("isIdle");

        //RaycastHit hit;
        //Ray ray = new Ray(transform.position, transform.forward * 1000);
        //Debug.DrawRay(transform.position,transform.forward * 1000, Color.green);   
        //if (Physics.Raycast(ray, out hit, 1000))
        //{
        //    Debug.Log(hit.transform.name);
        //    float _distance = Vector3.Distance(hit.transform.position, transform.position);
        //    Debug.Log(_distance);
        //    if(_distance < 5)
        //    {
        //        hit.transform.SendMessage("Hit", 100, SendMessageOptions.DontRequireReceiver);
        //    }
        //}
    }

    public override void Reload()
    {
        //Check if player is reloading
        if (!isReloading)
        {
            Debug.Log("A");
            //Check that the clip can carry ammo
            if (currentClipAmmo < maxClipSize)
            {
                //Determine how many bullets are needed
                int numOfBulletsToReload = maxClipSize - currentClipAmmo;
                isReloading = true;

                //Turn Off Reticle
                LevelUIManager.instance.TurnOffReticle();
                StartCoroutine(PlaceAmmo(numOfBulletsToReload));
                //Play Reloading Animation
                if (weaponAnim)
                    SetAnimationState("isReloading");
               
            }
        }
    }

    public override IEnumerator PlaceAmmo(int _numOfBulletsToReload)
    {
        for (int i = 0; i < _numOfBulletsToReload; i++)
        {
            yield return new WaitForSeconds(reloadTime);
            if (currentTotalAmmo > 0)
            {
                currentClipAmmo++;
                //Update Player UI
                int _currentClip = currentClipAmmo - 1;
                LevelUIManager.instance.TurnOnBulletIcons(_currentClip);

                currentTotalAmmo--;
                LevelUIManager.instance.UpdateTotalAmmo(currentTotalAmmo);             
            }
            else
            {
                Debug.Log("Out Of Ammo");
            }
        }

        //Wait For Animation To Finish
        yield return new WaitForSeconds(1.75F);

        isReloading = false;

        //Turn On Reticle
        LevelUIManager.instance.TurnOnReticle();

        //Return To Idle Animation
        if(weaponAnim)
            SetAnimationState("isIdle");
    }

    void SendOutRayCast()
    {
        //Play Shooting Animation
        if (weaponAnim)
            SetAnimationState("isShooting");

        //Particle System
        if (gunSystem)
        {
            gunSystem.Play();
            ParticleSystem.EmissionModule _em = gunSystem.emission;
            _em.enabled = true;
        }
       

        //Remove Ammo
        DecreaseAmmoCount();

        int _bulletIconId = currentClipAmmo;

        LevelUIManager.instance.TurnOffBulletIcons(_bulletIconId);

        //Add On Cooldown Time
        CooldownTimeIncrement();

        //Send Out Ray Cast From the Middle Of the Screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        //Send Out Bullet
        for(int i = 0; i < pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                //pooledBullets[i].transform.parent = gunMuzzle;
                pooledBullets[i].transform.position = gunMuzzle.position;
                pooledBullets[i].transform.localRotation = gunMuzzle.rotation;               
                pooledBullets[i].SetActive(true);
                break;
            }
        }

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            hit.collider.gameObject.SendMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);          
        }

        StartCoroutine(ReturnToIdle());

        //StartCoroutine(TurnOffParticleSystem(_em));
    }

    IEnumerator TurnOffParticleSystem(ParticleSystem.EmissionModule _em)
    {
        yield return new WaitForSeconds(0.15f);
        //Stop Particle System
        _em.enabled = false;
        gunSystem.Stop();

    }

    IEnumerator ReturnToIdle()
    {
        yield return new WaitForSeconds(shootingCooldownTime    );
        if(weaponAnim)
            SetAnimationState("isIdle");
    }
}
