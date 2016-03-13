using UnityEngine;
using System.Collections;

public class SwitchBehaviour : MonoBehaviour {

    public GameObject target;
    private bool isActivated;

    void Start()
    {
        Init();
    }

    void Init()
    {
        ResetManager.ResetLevel += Init;

        isActivated = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }    


	void Hit(float _damage)
    {
        if (!isActivated)
        {
            target.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
            isActivated = true;
            SetSwitchOn();
        }
    }

    void SetSwitchOn()
    {
        GetComponent<Animation>().Play("SwitchOn");
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
