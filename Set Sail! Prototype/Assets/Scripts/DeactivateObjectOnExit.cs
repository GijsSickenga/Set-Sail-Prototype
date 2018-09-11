using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObjectOnExit : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectToDeactivate;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
