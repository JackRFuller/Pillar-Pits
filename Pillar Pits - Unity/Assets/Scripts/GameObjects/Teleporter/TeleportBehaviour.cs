using UnityEngine;
using System.Collections;

public class TeleportBehaviour : MonoBehaviour {

    public Transform teleportTwin;
    private TeleportBehaviour twinTeleport;
    [HideInInspector] public bool hasEnteredTeleport;

    void Start()
    {
        Init();
    }

    void Init()
    {
        twinTeleport = teleportTwin.GetComponent<TeleportBehaviour>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasEnteredTeleport)
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.position = new Vector3(teleportTwin.position.x, teleportTwin.position.y + 1.5f, teleportTwin.position.z);
            twinTeleport.hasEnteredTeleport = true;
            Debug.Log("Hit");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (hasEnteredTeleport)
        {
            hasEnteredTeleport = false;
            twinTeleport.hasEnteredTeleport = false;
        }
          
    }
}
