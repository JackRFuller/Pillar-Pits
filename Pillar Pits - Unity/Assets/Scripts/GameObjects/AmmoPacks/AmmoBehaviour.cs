using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmmoBehaviour : MonoBehaviour {

	[SerializeField] private int ammoHeld;
    public Text ammoText;
    private bool hasBeenUsedUp;

    void Start()
    {
        Init();
    }

    void Init()
    {
        ResetManager.ResetLevel += Init;
        hasBeenUsedUp = false;
        ammoText.text = ammoHeld.ToString() + "x";
    }


    void OnTriggerEnter(Collider other)
    {
        if(!hasBeenUsedUp)
            if(other.tag == "Player")
            {
                PlayerShoot psScript = other.GetComponent<PlayerShoot>();
                psScript.currentTotalAmmo += ammoHeld;
                LevelUIManager.instance.UpdateTotalAmmo(psScript.currentTotalAmmo);
                hasBeenUsedUp = true;
                SetUI();
            }
    }

    void SetUI()
    {
        ammoText.text = "0x";
    }
}
