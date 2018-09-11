using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
	public int health = 9;
	public int maxHealth = 9;
    public Animator shipAnim;

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void Damage(int amount)
    {
        health -= amount;

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        shipAnim.GetComponent<Animator>().SetBool("isSinking", true);
    }

    public void EndDeathAnimation()
    {
        Destroy(gameObject);
    }
}
