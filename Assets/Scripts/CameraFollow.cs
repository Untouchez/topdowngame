using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public Transform target;
    public Vector3 offset;
    public Player player;
    public bool lockedOn;
    public Transform lookAt;
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

        if (lockedOn)
        {
            LookAt();
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 2f);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~playerMask))
        {
            Vector3 newPos = hit.point;
            newPos.y = 1f;
            lookAt.transform.position = newPos;
        }
    }

    public void LookAt()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.red,2f);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~playerMask))
        {
            player.LookAt(hit.point, player.rotateSpeed);
        }
    }
}
