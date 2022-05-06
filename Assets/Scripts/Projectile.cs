using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody rb;
    public ParticleSystem hitEffect;
    bool hitSomething;
    public float drop;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (hitSomething)
            return;
        rb.AddForce(Vector3.down * drop, ForceMode.Acceleration);
    }

    public void OnCollisionEnter(Collision collision)
    {
        this.hitSomething = true;
        this.rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        this.transform.position += (this.transform.forward*0.4f);
        ParticleSystem temp = Instantiate(hitEffect, transform.position,transform.rotation);
        Destroy(temp.gameObject, 1f);
    }
}
