using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private string playerName;
    [SerializeField] private int coins = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private int playerId;
    [SerializeField] private int currentXP = 1;
    [SerializeField] private int maxUnitCount = 1;
    [SerializeField] private List<Unit> units = new List<Unit>();
    [SerializeField] private List<int> xpRequirements = new List<int>();
    [SerializeField] private LevelData levelData;
    public Board board; // Assign in the Inspector if possible
    public Shop shop; // Assign in the Inspector if possible
    public Camera playerCamera; // Assign in the Inspector if possible

    public string PlayerName { get => playerName; set => playerName = value; }
    public int Coins { get => coins; set => coins = value; }
    public int Level { get => level; set => level = value; }
    public int PlayerId { get => playerId; set => playerId = value; }
    public int CurrentXP { get => currentXP; set => currentXP = value; }
    public int MaxUnitCount { get => maxUnitCount; set => maxUnitCount = value; }

    private void InitializeXpRequirements() {
        if (levelData != null) {
            xpRequirements = new List<int>(levelData.xpRequirements);
        }
        else {
            Debug.LogWarning("LevelData not assigned for player " + playerName);
        }
    }

    public void setupPlayer(string name, int id) {
        playerName = name;
        playerId = id;
        InitializeXpRequirements();

        if (board != null) board.BoardId = id;
        if (shop != null) shop.ShopId = id;
        if (playerCamera != null) {
            CameraLockOn cameraLockOnScript;
            if (playerCamera.TryGetComponent<CameraLockOn>(out cameraLockOnScript)) {
                cameraLockOnScript.setCameraId(id);
            }
        }

    }

    public void addCoins(int amount) {
        coins += amount;
    }

    public bool spendCoins(int amount) {
        if (coins >= amount) {
            coins -= amount;
            return true;
        }
        return false;
    }

    public int getCoins() {
        return coins;
    }

    public void addXP(int amount) {
        currentXP += amount;
        while (level - 1 < xpRequirements.Count && currentXP >= xpRequirements[level - 1]) {
            levelUp();
        }
    }

    public int getRequiredXp() {
        if (level == 1) {
            return xpRequirements[0];
        }
        return xpRequirements[level - 1];
    }

    public int getXp() {
        return currentXP;
    }

    private void levelUp() {
        level++;
        currentXP = 0;
        maxUnitCount++;
        board.updateLevelText();
    }

    public void AddUnit(Unit unit) {
        if (units.Count < MaxUnitCount) {
            units.Add(unit);
            unit.TeamId = playerId;
        }
        else {
            // Do nothing (for now)
        }
    }

}
