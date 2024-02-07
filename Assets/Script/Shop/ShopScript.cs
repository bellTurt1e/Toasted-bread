using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class ShopScript : MonoBehaviour { 
    [SerializeField] private int shopId;
    [SerializeField] private List<ShopItem> availableUnits = new List<ShopItem>();
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Player player;
    [SerializeField] private Transform shopPanel;
    [SerializeField] private Button[] buttons;

    void Start() {
        
        buttons = shopPanel.GetComponentsInChildren<Button>();
        foreach (var button in buttons) {
            button.onClick.AddListener(() => ButtonClicked(button));
            Debug.Log("Adding click lister to: " + button.name);
        }
        updateCoins();
        populateShop();
    }

    void ButtonClicked(Button button) {
        Debug.Log(button.name + " was clicked.");
        ShopItem shopItem = button.GetComponent<ShopItemButton>().getItem();
        if (shopItem.getCost() <= player.getCoins() && shopItem.getQuantity() > 0) {
            player.spendCoins(shopItem.getCost());
            updateCoins();
            button.interactable = false;
            button.GetComponent<ShopItemButton>().clearItem();
            button.GetComponentInParent<Image>().sprite = null;
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
