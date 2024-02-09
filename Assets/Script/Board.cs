using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] int boardId;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerLevel;
    [SerializeField] Player player;
    [SerializeField]public int totalTiles = 8; // The total number of available tiles for player.
    [SerializeField]private List<Transform> occupiedTiles = new List<Transform>();
    //public ShopItem
    [SerializeField] Unit prefab;
    public void Start() {
        setPlayerInfoOnBoard();
    }

    public void setBoardId(int boardId) {
        this.boardId = boardId;
    }

    public void setPlayerInfoOnBoard() {
        playerName.text = player.getPlayerName().ToString();
        updateLevelText();
    }

    public void updateLevelText() {
        playerLevel.text = player.getPlayerLevel().ToString();
    }

    public void SpawnUnit(Unit prefab)
    {
        // Check if there is an available tile
        if (occupiedTiles.Count < totalTiles)
        {
            // Find an available tile dynamically
            Transform availableTile = FindAvailableTile();

            if (availableTile != null)
            {
                // Spawn the unit at the available tile position
                Instantiate(prefab, availableTile.position, Quaternion.identity);

                // Mark the tile as occupied
                occupiedTiles.Add(availableTile);
            }
        }
        else
        {
            Debug.LogWarning("Bench is full. Cannot spawn more units.");
        }
    }

    private Transform FindAvailableTile()
    {
        // Iterate through all tiles and find the first available one
        foreach (Transform tile in transform)
        {
            if (!occupiedTiles.Contains(tile))
            {
                return tile; // Found an available tile
            }
        }

        return null; // No available tiles on the bench (bench is full)
    }
}
