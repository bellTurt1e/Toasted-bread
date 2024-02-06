using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public TextMeshProUGUI PriceTxt;
    public TextMeshProUGUI QuantittyTxt;
    public GameObject ShopManager;
    // Update is called once per frame
    void Update()
    {
        PriceTxt.text = "Price: $" + ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString();
        QuantittyTxt.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[3, ItemID].ToString();

    }
}
