using UnityEngine;
using System.Collections;

public class BreakawayFloorBehaviour : MonoBehaviour
{
    [SerializeField] private float breakawayDuration;
    private bool isPhasing;

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            if (!isPhasing)
                StartCoroutine(StartBreakAway());
        }
    }

    IEnumerator StartBreakAway()
    {
        isPhasing = false;

        MeshRenderer _mesh = GetComponent<MeshRenderer>();

        for(int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(breakawayDuration / 4);
            if( i < 4)
            {                
                if (_mesh.enabled)
                    _mesh.enabled = false;
                else _mesh.enabled = true;
            }
            else
            {
                BreakAway();
            }
        }
    }

    void BreakAway()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}
