using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private int boardId;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerLevel;
    [SerializeField] Player player;
    [SerializeField] private List<Transform> benchTiles = new List<Transform>();
    [SerializeField] private List<Transform> occupiedTiles = new List<Transform>();

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

    public void SpawnUnit(Unit prefab) {
        // Find an available tile dynamically
        Transform availableTileTransform = FindAvailableTile();

        if (availableTileTransform != null) {
            // Access the BoardTile component to get the correct top position for spawning the unit
            BoardTile availableTile = availableTileTransform.GetComponent<BoardTile>();
            if (availableTile != null) {
                Vector3 spawnPosition = availableTile.GetTileTopPosition(); // Get the top position from the BoardTile script

                // Spawn the unit at the calculated top position of the available tile
                Instantiate(prefab, spawnPosition, Quaternion.identity);
                Debug.Log("Spawning Unit at: " + spawnPosition);

                // Mark the tile as occupied by setting the unit
                availableTile.setOccupied(prefab); // Assuming prefab is of type Unit and setOccupied accepts a Unit type

                // Optionally, if you have a list of occupied tiles, you can add the Transform of the tile to it
                // occupiedTiles.Add(availableTileTransform);
            }
            else {
                Debug.LogError("Available tile does not have a BoardTile component.");
            }
        }
        else {
            Debug.Log("No available tile found.");
        }
    }


    private Transform FindAvailableTile() {
        foreach (Transform tile in benchTiles) {
            BoardTile tileComponent = tile.GetComponent<BoardTile>(); // Get the BoardTile component of the tile
            if (tileComponent != null && !tileComponent.getIsBenchTile() && !tileComponent.getOccupied()) { // Check if the tile is a bench tile
                return tile; // Return the first unoccupied tile
            }
        }

        Debug.Log("No available tile found!");
        return null; // No available tiles on the bench (bench is full)
    }
}
