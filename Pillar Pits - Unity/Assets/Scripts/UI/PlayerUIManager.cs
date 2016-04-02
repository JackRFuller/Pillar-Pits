using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIManager : MonoBehaviour {

    public static PlayerUIManager instance;

    [Header("Bullet Time")]
    [SerializeField] private Image bulletTimeMeter;
    [SerializeField] private float timeUntilTurnOff; //Wait Time Before Turning off the Meter

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start ()
    {
        Init();
	}	

    void Init()
    {
        bulletTimeMeter.gameObject.SetActive(false);
    }

    public void ChangeBulletTimeMeter(float _meter)
    {
        if (!bulletTimeMeter.gameObject.activeInHierarchy)
                bulletTimeMeter.gameObject.SetActive(true);

        float _fillAmount = _meter / 100.0f;
        if(_fillAmount < 0)
        {
            _fillAmount = 0;
        }

        if(_fillAmount >= 1)
        {
            _fillAmount = 1;
            StartCoroutine(WaitBeforeTurningOffBulletTime());
        }

        bulletTimeMeter.fillAmount = _fillAmount;

    }

    IEnumerator WaitBeforeTurningOffBulletTime()
    {
        yield return new WaitForSeconds(timeUntilTurnOff);
        bulletTimeMeter.gameObject.SetActive(false);
    }


}
