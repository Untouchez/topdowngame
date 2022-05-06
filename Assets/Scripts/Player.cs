using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("References")]
    public AudioSource audioSource;
    public AudioClip[] grassSounds;
    public Inventory inventory;
    public Animator anim;
    public Animator bowAnim;
    public GameObject bow;
    public Transform arrowSpawn;
    public Projectile arrow;

    public Collider rf;
    public Collider lf;

    [Header("Stats")]
    public float rotateSpeed;
    public bool isAttacking;
    public bool isSprint;
    public bool hasBow;
    public float bowForce;
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
        bow.SetActive(hasBow);
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
            anim.SetTrigger("attack");
        }
        if (Input.GetMouseButtonDown(1))
        {
            hasBow = !hasBow;
            bow.SetActive(hasBow);
            anim.SetBool("bow", hasBow);
            CF.lockedOn = hasBow;
        }
    }

    public void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprint = true;
            anim.SetBool("sprint", isSprint);
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
        audioSource.clip = grassSounds[Random.Range(0, grassSounds.Length)];
        audioSource.Play();
    }

    public void FootL()
    {
        audioSource.clip = grassSounds[Random.Range(0, grassSounds.Length)];
        audioSource.Play();
    }

    public void Draw()
    {
        bowAnim.Play("Draw", 0, 0);
    }

    public void Fire()
    {
        bowAnim.Play("Fire", 0, 0);
        Projectile newArrow = Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation);
        Vector3 destination = (CF.lookAt.position - arrowSpawn.position).normalized * 25f;
        //Vector3 destination = transform.forward * 50f;
        destination.y = 1f;
        newArrow.rb.AddForce(transform.forward* bowForce);
    }
    #endregion
}
