using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class Shop : MonoBehaviour { 
    [SerializeField] private int shopId;
    [SerializeField] private List<ShopItem> availableUnits = new List<ShopItem>();
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Player player;
    [SerializeField] private Transform shopPanel;
    [SerializeField] private Button[] buttons;
    //[SerializeField] private Transform[] playerBench;
    public Board board;

    void Start() {
        buttons = shopPanel.GetComponentsInChildren<Button>();
        foreach (var button in buttons) {
            button.onClick.AddListener(() => ButtonClicked(button));
        }
        updateCoins();
        populateShop();
        levelText.text = ("Level: " + player.getXp() + "/" + player.getRequiredXp()).ToString();
    }

    void ButtonClicked(Button button) {
        Debug.Log(button.name + " was clicked.");
        ShopItem shopItem = button.GetComponent<ShopItemButton>().getItem();
        Debug.Log("Shop item cost is: " + shopItem.getCost() + ". Player coin is at: "  +  player.getCoins() + ". And the shop item quantity is: " + shopItem.getQuantity());
        if (shopItem.getCost() <= player.getCoins() && shopItem.getQuantity() > 0) {
            player.spendCoins(shopItem.getCost());
            updateCoins();
            button.interactable = false;
            //Unit spawns on bench. make a spawn method. ... we're gonna get the unit prefab from shopTiemButton(Inharitence)
            board.SpawnUnit(button.GetComponent<ShopItemButton>().getItem().getUnit());
            button.GetComponent<ShopItemButton>().clearItem();
            button.GetComponentInParent<Image>().sprite = null;
        }
        
    }

    public void rerollShop() {
        if (player.getCoins() >= 2) {
            player.spendCoins(2);
            updateCoins();
            populateShop();    
        }  
    }

    public void addXp() {
        if (player.getCoins() >= 2) {
            player.addXP(2);
            player.spendCoins(2);
            updateCoins();

            levelText.text = ("Level: " + player.getXp() + "/" + player.getRequiredXp()).ToString();
        } 
    }

    public void setShopId(int shopId) { 
        this.shopId = shopId; 
    }
    public int getShopId() { 
        return shopId; 
    }

    public void updateCoins() {
       coinsText.text = player.getCoins().ToString();
    }


    public void populateShop() {
        foreach (Button button in buttons) {
            ShopItem unit = getRandomUnit();
            ShopItemButton shopItemButton = button.GetComponent<ShopItemButton>();
            if (shopItemButton != null) {
                shopItemButton.setItem(unit);
                button.GetComponentInParent<Image>().sprite = unit.getSprite();
                button.interactable = true;
            }
            else {
                Debug.LogError("ShopItemButton script not found on button");
            }
        }
    }


    public ShopItem getRandomUnit() {
        int randNum = Random.Range(0, availableUnits.Count);
        ShopItem unit = availableUnits[randNum];

        return unit;
    }
}
