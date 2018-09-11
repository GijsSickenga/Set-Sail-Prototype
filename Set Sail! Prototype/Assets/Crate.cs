using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {
    public float waterHeight = 0;
    public float upForce = 10f;

	void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // Pick up crate
            Destroy(gameObject);
        }
    }

    void Update()
    {
        float upwards = waterHeight - transform.position.y;
        if (upwards > 0)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * upwards / upForce);
        }
    }
}
