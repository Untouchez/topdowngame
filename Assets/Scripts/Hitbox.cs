using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public ParticleSystem hitEffect;
    string myTag;
    public int damage;
    public void Start()
    {
        myTag = transform.root.tag;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(myTag))
            return;
        if (other.CompareTag("Hittable")) {
            Health health = other.GetComponent<Health>();
            health.TakeDamage(damage,this.transform);
            ParticleSystem tempEffect = health.hitEffect;
            tempEffect.transform.position = other.ClosestPoint(transform.position + new Vector3(0,2,0));
            tempEffect.Play(true);
        } else {
            hitEffect.transform.position = other.ClosestPoint(transform.position + new Vector3(0, 2, 0));
            hitEffect.Play(true);
        }
    }
}
