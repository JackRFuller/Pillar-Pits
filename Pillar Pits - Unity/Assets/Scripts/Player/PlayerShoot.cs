using UnityEngine;
using System.Collections;

public class PlayerShoot : ShootingClass {

    void Start()
    {
        LevelUIManager.instance.UpdateTotalAmmo(currentTotalAmmo);
    }

    void Init()
    {

    }
	
	// Update is called once per frame
	void Update () {

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
            if(CooldownCheck())
                Reload();            
        }
	}

    public override void Reload()
    {
        //Check if player is reloading
        if (!isReloading)
        {
            //Check that the clip can carry ammo
            if (currentClipAmmo < maxClipSize)
            {
                //Determine how many bullets are needed
                int numOfBulletsToReload = maxClipSize - currentClipAmmo;
                isReloading = true;
                //Play Reloading Animation
                SetAnimationState("isReloading");
                StartCoroutine(PlaceAmmo(numOfBulletsToReload));
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

        //Return To Idle Animation
        SetAnimationState("isIdle");
    }

    void SendOutRayCast()
    {
        //Play Shooting Animation
        SetAnimationState("isShooting");

        //Remove Ammo
        DecreaseAmmoCount();

        int _bulletIconId = currentClipAmmo;

        LevelUIManager.instance.TurnOffBulletIcons(_bulletIconId);

        //Add On Cooldown Time
        CooldownTimeIncrement();

        //Send Out Ray Cast From the Middle Of the Screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            hit.collider.gameObject.SendMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);          
        }

        StartCoroutine(ReturnToIdle());
    }

    IEnumerator ReturnToIdle()
    {
        yield return new WaitForSeconds(shootingCooldownTime    );
        SetAnimationState("isIdle");
    }
}
