using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public float[] rotSpeeds = new float[3];
    public float rotSpeed;

	// Use this for initialization
	void Start () {

        for(int i = 0; i < rotSpeeds.Length; i++)
        {
            rotSpeeds[i] = Random.Range(1, 4);
        }
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 _rot = new Vector3(rotSpeeds[0], rotSpeeds[1], rotSpeeds[2]);
        transform.Rotate(_rot * rotSpeed);
	
	}
}
