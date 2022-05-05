using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
   
    [Header("References")]
    public Inventory inventory;
    public Animator anim;
    public Collider rf;
    public Collider lf;

    [Header("Stats")]
    public float rotateSpeed;
    public bool isAttacking;
    public bool isSprint;
    public bool isLock;
    public float pickupRange;

    Vector3 input;
    CameraFollow CF;
    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        CF = CameraFollow.instance;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        HandleAttack();

        Sprint();
        Roll();
        if (input != Vector3.zero)
        {
            CheckForItems();
        }
    }

    public void Roll()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetFloat("hitX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("hitY", Input.GetAxisRaw("Vertical"));

            anim.SetTrigger("roll");
        }
    }
    public void CheckForItems()
    {
        foreach(Transform child in nearby)
        {
            if (child == null)
            {
                nearby.Remove(child);
                return;
            }
            if (child.CompareTag("Pickup"))
            {
                Pickup currItem = child.GetComponent<Pickup>();
                if (currItem.pickedUp)
                    return;
                if (Vector3.Distance(child.transform.position,transform.position) <= pickupRange)
                {
                    currItem.pickedUp = true;
                    nearby.Remove(child);
                    currItem.OnPickUp();
                    return;
                }
            }
        }
    }


    public void HandleMovement()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        anim.SetFloat("Horizontal", input.x);
        anim.SetFloat("Vertical", input.y);
    }

    public void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            anim.SetTrigger("attack");
        }
    }

    public void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            CF.lockedOn = false;
            CF.lockOnTarget = null;
            isLock = false;
            isSprint = true;
            anim.SetBool("sprint", isSprint);
            anim.SetBool("lock", isLock);
        }
        else
        {
            isSprint = false;
            anim.SetBool("sprint", isSprint);
        }
    }

    public void LookAt(Vector3 target, float speed)
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = target - transform.position;
        
        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        newDirection.y = 0;
        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void LookAtDirection(Vector3 target, float speed)
    {
        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target, singleStep, 0.0f);
        newDirection.y = 0;
        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public List<Transform> nearby;
    public void OnTriggerEnter(Collider other)
    {
        nearby.Add(other.transform);
        if (other.CompareTag("Environment"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            foreach (Transform child in other.transform)
            {
                child.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        nearby.Remove(other.transform);
        if (other.CompareTag("Environment"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            foreach(Transform child in other.transform)
            {
                child.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    public void LockOn(bool val)
    {
        anim.SetBool("lock", val);
    }

    #region Events
    public void Anticipation()
    {
        anim.speed = 1f;
    }

    public void OpenColliders(string bodypart)
    {
        anim.speed = 1.4f;
        if(bodypart == "lf")
        {
            lf.enabled = true;
        }else if (bodypart == "rf")
        {
            rf.enabled = true;
        }
    }

    public void CloseColliders()
    {
        anim.speed = 1f;
        rf.enabled = false;
        lf.enabled = false;
    }

    public void FootR()
    {

    }

    public void FootL()
    {

    }
    #endregion
}
