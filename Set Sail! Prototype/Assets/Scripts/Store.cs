using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    enum Items
    {
        SHIELD = 0
    }

    public Text moneyText;

    public PlayerStock playerStock;

    public List<int> storeItemPrice;
    public List<int> storeItemStock;
    public Text storeItem1ShipStockText;
    public Text storeItem1PriceText;
    public Text storeItem1StockText;
    public Button storeItem1ButtonRight;
    public Button storeItem1ButtonLeft;

    private void Start()
    {
        storeItemPrice.Add(10);
        storeItemStock.Add(5);
        storeItem1ButtonRight.onClick.AddListener(delegate { BuyItem(Items.SHIELD);});
        storeItem1ButtonLeft.onClick.AddListener(delegate { SelItem(Items.SHIELD); });
        UpdateUI(Items.SHIELD);
    }

    private void BuyItem(Items item)
    {
        if(storeItemStock[(int)item] > 0 && playerStock.money >= storeItemPrice[(int)item])
        {
            playerStock.money -= storeItemPrice[(int)item];
            storeItemStock[(int)item]--;
            playerStock.itemShipStock[(int)item]++;
            UpdateUI(item);
        }
    }

    private void SelItem(Items item)
    {
        if (playerStock.itemShipStock[(int)item] > 0)
        {
            playerStock.money += storeItemPrice[(int)item];
            storeItemStock[(int)item]++;
            playerStock.itemShipStock[(int)item]--;
            UpdateUI(item);
        }
    }

    private void UpdateUI(Items item)
    {
        moneyText.text = playerStock.money.ToString();
        storeItem1PriceText.text = storeItemPrice[(int)item].ToString();
        storeItem1ShipStockText.text = playerStock.itemShipStock[(int)item].ToString();
        storeItem1StockText.text = storeItemStock[(int)item].ToString();
    }
}
