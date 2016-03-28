using UnityEngine;
using System.Collections;

[System.Serializable]
public class ShootingClass : MonoBehaviour {

    [Header("Shooting Attributes")]
    public float maxShootingRange; //Determines how far the actor can shoot
    private float shootingTime; //Determines if the player can shoot or not
    public float shootingCooldownTime; //Determines how much time the actor must wait inbetween shots to shoot

    [Header("Ammo Attributes")]
    public int currentClipAmmo; //Determines how much ammo the user has in their current clip
    public int currentTotalAmmo; //Determines how much ammo the user currentl has overall
    public int maxAmountOfAmmo; //Determines the amount of ammo the actor can have on their person
    public int maxClipSize; //Determines how much ammo is available in a single clip
    public float reloadTime; //Determines how long it takes to reload each individal bullet
    public bool isReloading;
    
    [Header("Damage")]
    public float damage; //Determines the amount of damage inflicted by the weapon    

    [Header("Animation")]
    public Animator weaponAnim;

    //Check that the current ammo amount is above 0
	public bool AmmoCheck()
    {
        if (currentClipAmmo > 0)
            return true;
        else return false;
    }

    public bool CooldownCheck()
    {
        if (Time.time > shootingTime)
            return true;
        else return false;
    }

    public void CooldownTimeIncrement()
    {
        shootingTime = Time.time + shootingCooldownTime;
    }

    //Decrease Ammo - Should be called after shooting
    public void DecreaseAmmoCount()
    {
        currentClipAmmo--;
        //Update UI
        LevelUIManager.instance.UpdateTotalAmmo(currentClipAmmo);

    }

    //Reload Weapon
    public virtual void Reload()
    {
        Debug.Log("Start Reloading");
        //Check if player is reloading
        if (!isReloading)
        {
            //Check that the clip can carry ammo
            if (currentClipAmmo < maxClipSize)
            {
                //Determine how many bullets are needed
                int numOfBulletsToReload = maxClipSize - currentClipAmmo;
                isReloading = true;
                StartCoroutine(PlaceAmmo(numOfBulletsToReload));
                //Play Reloading Animation
                if (weaponAnim)
                    SetAnimationState("isReloading");
                
            }
        }
       
    }

    public virtual IEnumerator PlaceAmmo(int _numOfBulletsToReload)
    {
        for (int i = 0; i < _numOfBulletsToReload; i++)
        {
            yield return new WaitForSeconds(reloadTime);
            if (currentTotalAmmo > 0)
            {
                currentClipAmmo++;
                currentTotalAmmo--;
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
        if(weaponAnim)
            SetAnimationState("isIdle");  
    }

    #region Animation

    public void SetAnimationState(string _animParameter)
    {
        weaponAnim.SetBool(_animParameter, true);

        for(int i = 0; i < weaponAnim.parameterCount; i++)
        {            
            string _parameterName = weaponAnim.parameters[i].name;

            if (_parameterName != _animParameter)
                weaponAnim.SetBool(_parameterName, false);
        }
    }

    #endregion
}
