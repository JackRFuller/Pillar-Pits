using UnityEngine;
using System.Collections;

public class BulletTime : MonoBehaviour
{
    [SerializeField] float currentSlowMo = 0;
    [SerializeField] float slowTimeAllowed = 2.0f;	
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Time.timeScale == 1.0f)
                    Time.timeScale = 0.3f;
            else
            {
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }            
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSlowMo = 0;
            Time.timeScale = 1.0f;
        }     

        if(Time.timeScale == 0.3)
        {
            currentSlowMo += Time.deltaTime;
        }

        if(currentSlowMo > slowTimeAllowed)
        {
            currentSlowMo = 0;
            Time.timeScale = 1.0f;
        }
	}
}
