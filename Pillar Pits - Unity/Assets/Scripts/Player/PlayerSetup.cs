using UnityEngine;
using System.Collections;

public class PlayerSetup : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        LevelManager.InitaliseLevel += Init;
    }
	
	void Init()
    {
        ResetManager.ResetLevel += Init;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.GetChild(0).localRotation = Quaternion.Euler(Vector3.zero);

        transform.position = LevelManager.instance.levelAttributes.startingPlayerPos;
        //Debug.Log(LevelManager.instance.levelAttributes.startingPlayerRot);
        ////transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        //    //(LevelManager.instance.levelAttributes.startingPlayerRot);
    }
}
