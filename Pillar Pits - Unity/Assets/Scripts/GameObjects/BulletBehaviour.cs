using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float range; //Calculated in Seconds
    public LayerMask collisionMask;
    private Vector3 target;
    private Rigidbody rb;
    private bool isMoving;
    private bool hasCollided;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }    

    public void SetTarget(Vector3 _target)
    {
        target = _target;
        isMoving = true;
        direction = target - transform.position;

        StartCoroutine(CalculateDistance());
    }

    //void Update()
    //{
    //    if (isMoving)
    //        Reflection();
    //}

    void FixedUpdate()
    {
        if (isMoving)
        {
            MoveBullet();            
        }
            
    }

    void MoveBullet()
    {       
        rb.AddRelativeForce(direction * speed, ForceMode.Force);
    }

    void Reflection()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.green);
        RaycastHit hit;
        Debug.Log("A ");

        if (Physics.Raycast(ray, out hit,1f, collisionMask))
        {
            //Debug.Log("Hit " + hit.collider.name);
            if (hit.collider.tag == "Untagged")
            {
                Debug.Log("Hit " + hit.collider.name);
                Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
                float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, rot, 0);
                isMoving = false;
                hasCollided = true;
            }
        }

        if (hasCollided)
        {
            transform.Translate(Vector3.forward * speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            SendDamage(other.gameObject);
        }
    }

    void SendDamage(GameObject _target)
    {
        _target.SendMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);
        TurnOffBullet();
    }

    IEnumerator CalculateDistance()
    {
        yield return new WaitForSeconds(range);
        TurnOffBullet();
    }

    void TurnOffBullet()
    {
        if (rb)
            rb.velocity = Vector3.zero;
        direction = Vector3.zero;
        target = Vector3.zero;
        isMoving = false;
        hasCollided = false;
        
        gameObject.SetActive(false);
    }

}
