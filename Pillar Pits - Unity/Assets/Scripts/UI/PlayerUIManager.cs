using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIManager : MonoBehaviour {

    public static PlayerUIManager instance;

    [Header("Bullet Time")]
    [SerializeField] private Image bulletTimeMeter;

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
	void Start () {
	
	}	

    public void ChangeBulletTimeMeter(float _meter)
    {
        float _fillAmount = _meter / 100.0f;
        if(_fillAmount < 0)
        {
            _fillAmount = 0;
        }
        bulletTimeMeter.fillAmount = _fillAmount;
    }
}
