using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {
    public float waterHeight = 0;
    public float upForce = 12.72f;

	void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // Pick up crate
            other.gameObject.GetComponent<QuestManager>().ProgressQuest();
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        float upwards = waterHeight - transform.position.y;
        if (upwards > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(upForce * Vector3.up, ForceMode.Acceleration);
            GetComponent<Rigidbody>().drag = 0.3f;
        }
        else
        {
            GetComponent<Rigidbody>().drag = 0.05f;
        }
    }
}
