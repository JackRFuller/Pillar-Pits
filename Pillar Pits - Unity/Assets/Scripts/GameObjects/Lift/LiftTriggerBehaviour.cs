using UnityEngine;

public class LiftTriggerBehaviour : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            other.transform.parent = transform.parent;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            other.transform.parent = null;
    }
}
