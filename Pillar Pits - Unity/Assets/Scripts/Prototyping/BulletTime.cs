using UnityEngine;
using System.Collections;

public class BulletTime : MonoBehaviour
{
    private enum bulletTimeModes
    {
        Once,
        Recharge,
    }

    [SerializeField] private bulletTimeModes currentBulletTimeMode;

    [Header("Slow Mo Editable Values")]   
    [SerializeField] private float slowTimeAllowed = 2.0f;
    [SerializeField] private float timeBeforeRechargeTime = 2.0f;    
    private bool isRecharging;
    private float currentSlowMo = 0;

    void Start()
    {
        Init();
    }

    void Init()
    {
        currentSlowMo = slowTimeAllowed;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Start Bullet Time
        if (Input.GetKeyDown(KeyCode.E))
        {
            RunBulletTime();
            StopCoroutine(StartRecharging());
            isRecharging = false;              
        }

        //Stop Bullet Time
        if (Input.GetKeyUp(KeyCode.E))
        {
            StopBulletTime();
        }

        //Check Bullet Time Hasn't Run Out
        if(Time.timeScale == 0.3f)
        {
            currentSlowMo -= Time.deltaTime;

            float _percentageTaken = (currentSlowMo / slowTimeAllowed) * 100.0f;
            PlayerUIManager.instance.ChangeBulletTimeMeter(_percentageTaken);

            if (currentSlowMo <= 0)
            {
                StopBulletTime();
            }
        }

        //Recharge BulletTime
        if (isRecharging)
            RechargeBulletTime();
	}

    void RunBulletTime()
    {        
        if(Time.timeScale == 1.0f)
        {
            Time.timeScale = 0.3f;
        }            
        else
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;            
        }  
    }

    void StopBulletTime()
    {
       
        Time.timeScale = 1.0f;
        //Start Bullet Time Recharge
        if (currentBulletTimeMode == bulletTimeModes.Recharge)
            StartCoroutine(StartRecharging());     
    }

    IEnumerator StartRecharging()
    {
        yield return new WaitForSeconds(timeBeforeRechargeTime);
        isRecharging = true;
    }

    void RechargeBulletTime()
    {
        currentSlowMo += Time.deltaTime;
        float _percentageTaken = (currentSlowMo / slowTimeAllowed) * 100.0f;
        PlayerUIManager.instance.ChangeBulletTimeMeter(_percentageTaken);

        if (currentSlowMo >= slowTimeAllowed)
        {
            currentSlowMo = slowTimeAllowed;
            isRecharging = false;           
        }
    }


}
