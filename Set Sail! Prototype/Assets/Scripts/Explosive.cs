using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public GameObject explosionEffect;
    public GameObject fireEffect;
    public GameObject splashEffect;

    public int damage = 1;

    private bool _hasSplashed = false;

    private bool _resettingSplash = false;

    private void OnCollisionEnter(Collision collision)
    {
        // Prevent multiple collision calls in a row.
        if (!enabled)
            return;

        ContactPoint contact = collision.contacts[0];
        Vector3 pos = contact.point;

        ShipStats shipStats = null;
        if (contact.otherCollider.transform.parent != null)
        {
            if (contact.otherCollider.transform.parent.parent != null)
            {
                shipStats = contact.otherCollider.transform.parent.parent.GetComponent<ShipStats>();
            }
        }

		if (shipStats != null)
		{
            // Hit a ship, so damage it.
            shipStats.Damage(damage);

            // Explosion FX.
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Instantiate(fireEffect, transform.position, Quaternion.identity, contact.otherCollider.transform);
		}
        else
        {   
            // Hit an object that is not a ship.
            // Explosion FX.
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Instantiate(fireEffect, transform.position, Quaternion.identity, contact.otherCollider.transform);
        }

        // Unparent smoke trail so it doesn't disappear instantly when the cannonball is destroyed.
        GameObject smokeTrail = transform.GetChild(0).gameObject;
        smokeTrail.transform.parent = null;
        // Add a temp particle script to destroy it after a few seconds.
        smokeTrail.AddComponent<TempParticle>();

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_hasSplashed && other.CompareTag("WaterSurface"))
        {
            // Collided with water surface: display splash effect.
            Instantiate(splashEffect, transform.position, Quaternion.identity);
            _hasSplashed = true;
        }
    }

    private void Update()
    {
        // Destroy cannonball if it falls too low below the world.
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }

        if (_hasSplashed && !_resettingSplash)
        {
            StartCoroutine(ResetSplashFlag());
        }
    }

    private IEnumerator ResetSplashFlag()
    {
        _resettingSplash = true;
        yield return new WaitForSeconds(1.0f);
        _hasSplashed = false;
        _resettingSplash = false;
        yield break;
    }
}
