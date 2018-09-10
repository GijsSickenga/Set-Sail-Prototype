using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStock : MonoBehaviour
{
    public int money;
    public List<int> itemShipStock;

    private void Start()
    {
        itemShipStock.Add(5);
    }
}
