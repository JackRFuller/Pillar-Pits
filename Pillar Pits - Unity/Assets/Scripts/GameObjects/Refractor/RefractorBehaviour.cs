using UnityEngine;

public class RefractorBehaviour : MonoBehaviour {

	public void Hit(float _damage)
    {        
        Ray ray = new Ray(transform.position, transform.right);

        SendOutRay(ray);       

        ray = new Ray(transform.position, -transform.right);

        SendOutRay(ray);
    }

    void SendOutRay(Ray _ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Target")
            {
                hit.collider.gameObject.SendMessage("Hit", 1000, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
