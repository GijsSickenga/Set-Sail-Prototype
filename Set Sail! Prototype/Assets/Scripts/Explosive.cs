using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
	public AudioSource explosion;

    public int damage = 1;

    void OnCollisionEnter(Collision collision)
    {
        // Prevent multiple collision calls in a row.
        if (!enabled)
            return;

        ContactPoint contact = collision.contacts[0];
        Vector3 pos = contact.point;
        // Instantiate explosion here...

        ShipStats shipStats = contact.otherCollider.GetComponent<ShipStats>();
		if (shipStats != null)
		{
            shipStats.health -= damage;
		}

        explosion.Play();

        Destroy(gameObject);
    }
}
