using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public int maxHealth;
    public int currentHealth;
    public bool isDamagable;

    public abstract void TakeDamage(int damage, Transform other);
    public abstract void Die();

}
