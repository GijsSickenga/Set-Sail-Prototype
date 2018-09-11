using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
	public AudioSource explosion;

    public GameObject explosionEffect;
    public GameObject splashEffect;

    public int damage = 1;

    private void OnCollisionEnter(Collision collision)
    {
        // Prevent multiple collision calls in a row.
        if (!enabled)
            return;

        ContactPoint contact = collision.contacts[0];
        Vector3 pos = contact.point;

        ShipStats shipStats = contact.otherCollider.GetComponent<ShipStats>();
		if (shipStats != null)
		{
            shipStats.health -= damage;
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
		}
        else
        {
            Instantiate(splashEffect, transform.position, Quaternion.identity);
        }

        // Play explosion sound effect.
        explosion.Play();

        Destroy(gameObject);
    }

    private void Update()
    {
        // Destroy cannonball if it falls too low below the world.
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }
}
