using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectOnEnter : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameObjectToActivate.SetActive(true);
        }
    }
}
