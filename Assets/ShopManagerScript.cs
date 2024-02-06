using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public int [,] shopItems = new int[6,6];
    public float coins;
    public TextMeshProUGUI CoinsTXT;
    public GameObject[] unitPrefabs = new GameObject[5]; // Drag your unit prefabs into this array in the Unity Editor.
    void Start()
    {
        CoinsTXT.text = "Coins:" + coins.ToString();
        shopItems[1,1] = 1;
        shopItems[1,2] = 2;
        shopItems[1,3] = 3;
        shopItems[1,4] = 4;
        shopItems[1,5] = 5;

        //price
        shopItems[2,1] = 1;
        shopItems[2,2] = 2;
        shopItems[2,3] = 3;
        shopItems[2,4] = 4;
        shopItems[2,5] = 2;

        //Quantity
        shopItems[3,1] = 10;
        shopItems[3,2] = 10;
        shopItems[3,3] = 10;
        shopItems[3,4] = 10;
        shopItems[3,5] = 20;
    }

    public void Buy()
    {
        GameObject ButtonREF = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (coins >= shopItems[2,ButtonREF.GetComponent<ButtonInfo>().ItemID] && coins != 0)
        {
            coins -= shopItems[2,ButtonREF.GetComponent<ButtonInfo>().ItemID];
            shopItems[3,ButtonREF.GetComponent<ButtonInfo>().ItemID]--;
            CoinsTXT.text = "Coins:" + coins.ToString();
            ButtonREF.GetComponent<ButtonInfo>().QuantittyTxt.text = shopItems[3,ButtonREF.GetComponent<ButtonInfo>().ItemID].ToString();
        }
    }
    public void SpawnUnit(int unitIndex)
    {
        if (unitIndex >= 0 && unitIndex < unitPrefabs.Length)
        {
            // Instantiate the unit prefab at a specific position (you can modify this)
            Vector3 spawnPosition = new Vector3(0f, 1f, 0f); // Adjust the position as needed
            Quaternion spawnRotation = Quaternion.identity; // No rotation

            GameObject newUnit = Instantiate(unitPrefabs[unitIndex], spawnPosition, spawnRotation);
        }
        else
        {
            Debug.LogError("Invalid unit index");
        }
    }

    void Update()
    {
        
    }
}
