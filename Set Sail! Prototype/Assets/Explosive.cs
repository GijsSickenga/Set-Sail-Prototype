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

        ShipControls ship = contact.otherCollider.GetComponent<ShipControls>();
		if (ship != null)
		{
			ship.health -= damage;
		}

        explosion.Play();

        Destroy(gameObject);
    }
}
