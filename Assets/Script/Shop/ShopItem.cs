using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopItem : MonoBehaviour {
    [SerializeField] string unitName;
    [SerializeField] Unit unit;
    [SerializeField] Sprite prefabIcon;
    [SerializeField] int cost;
    [SerializeField] int quantity;

    public string getUnitName() { 
        return unitName; 
    }    

    public Unit getUnit() {
        return unit;
    }

    public Sprite getSprite() {
        return prefabIcon;
    }

    public int getCost() {
        return this.cost;
    }

    public int getQuantity() {
        return this.quantity;
    }
    public void decreaseQuantity() {
        quantity--;
    }
}
