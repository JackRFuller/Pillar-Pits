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

    [Header("Reticle")]
    public Image reticleIcon;
    private bool isReticleRed;

    [Header("LevelOverUI")]
    public Animator levelOverAnim;
    public Text currentTimeText;
    public Text timeToNextStarText;
    public Image[] starIcons = new Image[3];

    [Header("Game Object Level Screen")]
    public GameObject loadingScreen;
       

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

    void Start()
    {
        ResetManager.ResetLevel += ResetLevelOverUI;
    }

    public void TurnOffLoadScreen()
    {
        loadingScreen.SetActive(false);
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

    public void TurnOnTargets(int _numberOfTargets)
    {
        for(int i = 0; i < _numberOfTargets; i++)
        {
            targetIconImages[i].enabled = true;
        }
    }

    public void TurnReticleRed()
    {
        if (reticleIcon.color != Color.red)
               reticleIcon.color = Color.red;
    }

    public void TurnReticleWhite()
    {
        if (reticleIcon.color != Color.white)
            reticleIcon.color = Color.white;
    }

    public void InitialiseNumberOfBullets(int _numberOfBullets)
    {
        for(int i = 0; i < bulletIconImages.Length; i++)
        {
            bulletIconImages[i].enabled = false;
        }

        for(int i = 0; i < _numberOfBullets; i++)
        {
            bulletIconImages[i].enabled = true;
        }
    }

    public void SetReticleState()
    {
        if (reticleIcon.enabled)
            reticleIcon.enabled = false;
        else reticleIcon.enabled = true;
    }

    public void SetupLevelOverUI(float _currentTime)
    {
        //Turn On UI
        levelOverAnim.transform.parent.root.gameObject.SetActive(true);

        //Turn Off Reticle
        SetReticleState();

        //Set Current Time
        currentTimeText.text = _currentTime.ToString("F2");

        levelOverAnim.SetBool("isShowingMenu", true);

        StartCoroutine(ShowStars(_currentTime));
    }

    IEnumerator ShowStars(float _playerTime)
    {
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < starIcons.Length; i++)
        {            
            yield return new WaitForSeconds(0.25F);
            if (_playerTime < LevelManager.instance.levelAttributes.starTimes[i])
                starIcons[i].enabled = true;
            else
            {
                float _timeToNextStar = -(LevelManager.instance.levelAttributes.starTimes[i] - _playerTime);
                timeToNextStarText.text = _timeToNextStar.ToString("F2") + "To Next Star";
                timeToNextStarText.enabled = true;
                break;
            }            
        }
    }

    void ResetLevelOverUI()
    {
        //Turn On UI
        levelOverAnim.transform.parent.root.gameObject.SetActive(false);

        if(levelOverAnim)
            levelOverAnim.SetBool("isShowingMenu", false);
        levelOverAnim.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1080);
        for(int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].enabled = false;
        }

        timeToNextStarText.enabled = false;
    }

    
}
