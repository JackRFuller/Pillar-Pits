using UnityEngine;
using System.Collections;

public class Hookshot : MonoBehaviour {

    private Rigidbody rb;
    [Header("HookShot Attributes")]
    [SerializeField] private float hookshotCoolDown;
    private float cooldownTime;
    private bool isMoving;
    private Vector3 hitPoint;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();
        cooldownTime = Time.time;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(1))
        {
            if(Time.time > cooldownTime)
                ShootHookShot();
        }
	}

    void ShootHookShot()
    {
        //Send Out Ray Cast From the Middle Of the Screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Work Out Hit Point
            hitPoint = hit.point;
            isMoving = true;
            rb.velocity = Vector3.zero;
            cooldownTime = Time.time + hookshotCoolDown;
            Debug.Log("Success");
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.AddForce(hitPoint * 10000);
        }
    }
}
