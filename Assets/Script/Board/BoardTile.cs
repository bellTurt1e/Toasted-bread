using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour {
    [SerializeField] private bool isBenchTile = false;
    [SerializeField] private bool isOccupied = false;
    [SerializeField] private Unit unit;

    public void Start() {
        GetTileTopPosition();
    }

    public Vector3 GetTileTopPosition() {
        Vector3 topPosition = transform.position + Vector3.up * (transform.localScale.y / 2f);
        topPosition.y += 2.3f;
        Debug.Log("The top center position is at: " + topPosition);
        return topPosition;
    }

    public bool getIsBenchTile() {
        return isBenchTile;
    }

    public void setNotOccupied() {
        isOccupied = false;
        unit = null;
    }

    public void setOccupied(Unit unit) {
        isOccupied = true;
        this.unit = unit;
    }

    public bool getOccupied() {
        return isOccupied;
    }
}
