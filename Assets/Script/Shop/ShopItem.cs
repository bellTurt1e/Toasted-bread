using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopItem : MonoBehaviour {
    [SerializeField] string unitName;
    [SerializeField] Unit prefab;
    [SerializeField] Sprite prefabIcon;
    [SerializeField] int cost;
    [SerializeField] int quantity;

    public string getUnitName() { 
        return unitName; 
    }    

    public Unit getPrefab() {
        return prefab;
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
