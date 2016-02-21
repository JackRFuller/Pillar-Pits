using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelUIManager : MonoBehaviour {

    public static LevelUIManager instance;

    [Header("Ammo Objects")]
    public Image[] bulletIconImages;
    public Text ammoTotalText;

    [Header("Level Timer")]
    public Text levelTimerText;

    [Header("Target Objects")]   
    public Image[] targetIconImages;

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

    public void UpdateTimer(float _currentTime)
    {
        levelTimerText.text = _currentTime.ToString("F2");
    }

    public void TurnOffBulletIcons(int _BulletToTurnOff)
    {
        bulletIconImages[_BulletToTurnOff].enabled = false;
    }

    public void TurnOnBulletIcons(int _BulletToTurnOn)
    {
        bulletIconImages[_BulletToTurnOn].enabled = true;
    }

    public void UpdateTotalAmmo(int _totalAmmo)
    {
        ammoTotalText.text = _totalAmmo.ToString();
    }

    public void TurnOffTargets(int numberOfTargets)
    {
        int _targetID = numberOfTargets;

        targetIconImages[_targetID].enabled = false;
    }
}
