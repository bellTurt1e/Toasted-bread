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
    public Button[] shopButtons = new Button[5]; // Drag your shop buttons into this array in the Unity Editor.
    public Transform shopPanel; // Drag your shop panel into this field in the Unity Editor.
    public Transform[] benchTiles; // Assign the bench tile positions in the Unity Editor
    private bool[] occupiedTiles; // To track which tiles are occupied on the bench

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
        InitializeBenchTiles();
        ShuffleShopItems();
        ShuffleShopButtons();
        UpdateShopUI();

    }

    public void Buy()
    {
        GameObject ButtonREF = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (coins >= shopItems[2, ButtonREF.GetComponent<ButtonInfo>().ItemID])
        {
            coins -= shopItems[2, ButtonREF.GetComponent<ButtonInfo>().ItemID];

            // Find the first available tile on the bench
            int availableTileIndex = FindAvailableBenchTileIndex();

            if (availableTileIndex != -1)
            {
                // Spawn the unit at the available tile position
                GameObject newUnit = Instantiate(unitPrefabs[shopItems[1, ButtonREF.GetComponent<ButtonInfo>().ItemID]], benchTiles[availableTileIndex].position, Quaternion.identity);

                // Mark the tile as occupied
                occupiedTiles[availableTileIndex] = true;

                // Update UI and other logic as needed
                CoinsTXT.text = "Coins:" + coins.ToString();
                ButtonREF.GetComponent<ButtonInfo>().QuantittyTxt.text = shopItems[3, ButtonREF.GetComponent<ButtonInfo>().ItemID].ToString();
            }
            else
            {
                // Handle the case when all bench tiles are occupied
                Debug.LogWarning("Bench is full. Cannot spawn more units.");
            }
        }
    }
    int FindAvailableBenchTileIndex()
    {
        // Iterate through bench tiles and find the first available one
        for (int i = 0; i < occupiedTiles.Length; i++)
        {
            if (!occupiedTiles[i])
            {
                return i; // Found an available tile
            }
        }

        return -1; // No available tiles on the bench (bench is full)
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
    public void ShuffleShopButtons()
    {
        // Randomize the order of shopButtons
        System.Random rng = new System.Random();
        int n = shopButtons.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Button value = shopButtons[k];
            shopButtons[k] = shopButtons[n];
            shopButtons[n] = value;
        }
    }

    public void ShuffleShopItems()
    {
        // Randomize the order of units in the shop
        System.Random rng = new System.Random();
        int n = unitPrefabs.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = shopItems[1, k + 1];
            shopItems[1, k + 1] = shopItems[1, n + 1];
            shopItems[1, n + 1] = value;
        }
    }

    void UpdateShopUI()
    {
        CoinsTXT.text = "Coins: " + coins.ToString();

        // Clear existing items in the shop panel
        foreach (Transform child in shopPanel)
        {
            Destroy(child.gameObject);
        }

        // Traverse through shuffled shop buttons and create UI elements for each item in the panel
        for (int i = 0; i < shopButtons.Length; i++)
        {
            // Instantiate the shuffled button and set its parent to the shop panel
            Button shopButton = Instantiate(shopButtons[i], shopPanel);
        }
    }

    public void DestroyButton(Button button)
    {
        // Check if the button exists
        if (button != null)
        {
            // Destroy the button's GameObject
            Destroy(button.gameObject);

            // Optionally, you might want to update your UI or perform other actions after destroying the button
            // UpdateShopUI(); // Uncomment this line if you have a function to update the shop UI
        }
    }
    void InitializeBenchTiles()
    {
        // Initialize the array to track bench tile occupation
        occupiedTiles = new bool[benchTiles.Length];
    }
    
    public void RefreshShop()
    {
        ShuffleShopButtons();
        UpdateShopUI();
    }

    void Update()
    {
        
    }
}
