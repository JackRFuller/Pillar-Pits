using UnityEngine;
using System.Collections;

public class AmmoUIBehaviour : MonoBehaviour {

    private Transform PC;

	// Use this for initialization
	void Start () {

        Init();
	
	}

    void Init()
    {
        PC = SceneManager.instance.PC.transform;
    }
	
	// Update is called once per frame
	void Update () {

        LookAtPC();
	    
	}

    void LookAtPC()
    {
        float _dist = Vector3.Distance(PC.position, transform.position);

        if(_dist > 2.25f)
        {
            transform.LookAt(new Vector3(2 * PC.transform.position.x,
                                    transform.position.y,
                                    PC.transform.position.z));
        }
    }
}
