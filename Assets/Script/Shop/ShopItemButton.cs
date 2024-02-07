using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour {
    public ShopItem item;

    public void setItem(ShopItem newItem) {
        item = newItem;
    }

    public ShopItem getItem() { 
        return item;
    }

    public void clearItem() {
        item = null;    
    }

   
}
