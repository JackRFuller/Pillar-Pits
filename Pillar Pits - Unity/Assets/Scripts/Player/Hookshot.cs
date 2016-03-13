using UnityEngine;
using System.Collections;

public class Hookshot : MonoBehaviour {

    
    [Header("DoubleClickAttributes")]
    [SerializeField] private float timeForDoubleClickCheck;
    private bool oneClick;
    private float doubleClickTimer;
    private bool hasDoubleClicked;

    [Header("Hookshot Attributes")]
    [SerializeField] private float hookshotSpeed;
    [SerializeField] private float hookshotModifier;   
    private Vector3 target;   
   

    //Character Attributes
    private CharacterController cc;
    public LineRenderer lr;
    private bool isMoving;

    void Start()
    {
        cc = GetComponent<CharacterController>();
       
    }
   

    void Update()
    {
       
        HookShotInput();

        if (isMoving)
            MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        Vector3 _offset = target - transform.position;
        float speed = hookshotSpeed;
        if (_offset.magnitude > .2f)
        {
            if (hasDoubleClicked)
            speed = hookshotSpeed * hookshotModifier;
            _offset = _offset.normalized * speed;
            cc.Move(_offset * Time.deltaTime);
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, target);
        }
    }

    void HookShotInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!oneClick)
            {
                oneClick = true;
                doubleClickTimer = Time.time;
                Debug.Log("Single Click");
                ShootHookShot(false);
            }
            else
            {
                oneClick = false;
                Debug.Log("DoubleClick");
                ShootHookShot(true);
            }
        }
        if (oneClick)
        {
            if ((Time.time - doubleClickTimer) > timeForDoubleClickCheck)
            {
                oneClick = false;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            isMoving = false;
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
            hasDoubleClicked = false;
        }
    }

    void ShootHookShot(bool _doubleClicked)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider)
            {
                target = hit.point;
                isMoving = true;

                if (_doubleClicked)
                    hasDoubleClicked = true;
                else hasDoubleClicked = false;
                 
            }
        }
    }
}
