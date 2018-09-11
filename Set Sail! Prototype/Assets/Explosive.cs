using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
	public AudioSource explosion;

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 pos = contact.point;
        // Instantiate explosion here...

        ShipControls ship = contact.otherCollider.GetComponent<ShipControls>();
		if (ship != null)
		{
			ship.health -= 1;
		}

        explosion.Play();

        Destroy(gameObject);
    }
}
