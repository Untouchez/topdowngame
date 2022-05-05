using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{
    public ParticleSystem death;
    public InventoryItemData stone;
    public Animator anim;
    Player player;

    public void Start()
    {
        player = Player.instance;
    }

    public override void Die()
    {
        foreach(Transform child in death.transform)
        {
            death.Emit(20);
        }
        player.inventory.Add(stone, 20);
        isDamagable = false;
        Destroy(this.gameObject,2f);
    }

    public override void TakeDamage(int damage, Transform other)
    {
        if (!isDamagable)
            return;
        currentHealth -= damage;
        if (currentHealth <= 0) {
            Die();
        }
        print(this.name + " took " + damage);
        Vector3 attackDir = (other.position - transform.position)*10f;
        anim.SetFloat("hitX", attackDir.x);
        anim.SetFloat("hitY", attackDir.z);
        anim.SetTrigger("hit");
    }
}
