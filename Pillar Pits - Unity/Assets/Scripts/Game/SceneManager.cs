using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        ResetScene();
    }

    void ResetScene()
    {
        if (Input.GetKey(KeyCode.R))
        {
             Application.LoadLevel(Application.loadedLevel);
        }
    }
}
