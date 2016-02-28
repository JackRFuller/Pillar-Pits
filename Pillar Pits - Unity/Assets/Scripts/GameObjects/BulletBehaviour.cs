using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    [SerializeField] private float bulletSpeed;
    private Rigidbody rb;
    private TrailRenderer tr;

	void OnEnable()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody>();

        //if (tr == null)
        //    tr = GetComponent<TrailRenderer>();

        

        //tr.transform.position = Vector3.zero;

        StartCoroutine(TurnOffBullet());
    }

    IEnumerator TurnOffBullet()
    {
        //tr.time = 5f;
        transform.parent = null;
        rb.velocity = transform.TransformPoint(Vector3.left * bulletSpeed);


        yield return new WaitForSeconds(0.2f);
       // tr.time = 0.1f;
        gameObject.SetActive(false);
    }
}
