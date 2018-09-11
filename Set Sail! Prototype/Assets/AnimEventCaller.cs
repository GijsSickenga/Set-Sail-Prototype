using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventCaller : MonoBehaviour {
    public GameObject mainObject;

    private void CallEndDeath()
    {
        mainObject.GetComponent<ShipStats>().EndDeathAnimation();
    }
}
