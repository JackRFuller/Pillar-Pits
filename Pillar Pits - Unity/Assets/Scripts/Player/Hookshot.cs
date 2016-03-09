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

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time > cooldownTime && !isMoving)
                ShootHookShot();
        }

        if (Input.GetMouseButtonUp(1))
        {
            isMoving = false;
            rb.velocity = Vector3.zero;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {       

        if (isMoving)
        {
            rb.AddForce((hitPoint - transform.position) * 2, ForceMode.Acceleration);
            float _dist = Vector3.Distance(hitPoint, transform.position);

            if (_dist < 7)
            {
                isMoving = false;
                rb.velocity = Vector3.zero;
            }
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
}
