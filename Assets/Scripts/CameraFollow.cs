using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public Transform target;
    public Vector3 offset;
    public Player player;

    public Transform lockOnTarget;
    public bool lockedOn;

    public LayerMask playerMask;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = Player.instance;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        if (Input.GetMouseButtonDown(1))
        {
            lockedOn = !lockedOn;

            if (lockedOn) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                ray.direction = ray.direction * 20f;
                //Debug.DrawRay(ray.origin, ray.direction,Color.red,5f);
                if (Physics.Raycast(ray, out RaycastHit hit,Mathf.Infinity,~playerMask)) {
                    lockOnTarget = hit.transform;
                    player.LockOn(true);
                } else {
                    lockOnTarget = null;
                    player.LockOn(false);
                    lockedOn = false;
                }
            } else {
                lockOnTarget = null;
                player.LockOn(false);
            }
        }

        //var lookAtPos = Input.mousePosition;
        //lookAtPos.z = Camera.main.transform.position.y - player.transform.position.y;
        //lookAtPos = Camera.main.ScreenToWorldPoint(lookAtPos);
        //player.LookAtDirection(lookAtPos - player.transform.position, 10f);
        if (lockOnTarget)
            player.LookAt(lockOnTarget.position, player.rotateSpeed);
    }
}
