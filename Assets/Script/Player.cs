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
        PlayerName = name;
        PlayerId = id;
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

    public bool spendCoins(int amount) {
        if (Coins >= amount) {
            Coins -= amount;
            return true;
        }
        return false;
    }

    public void addXP(int amount) {
        CurrentXP += amount;
        while (Level - 1 < xpRequirements.Count && CurrentXP >= xpRequirements[level - 1]) {
            levelUp();
        }
    }

    public int getRequiredXp() {
        if (Level == 1) {
            return xpRequirements[0];
        }
        return xpRequirements[level - 1];
    }

    public int getXp() {
        return CurrentXP;
    }

    private void levelUp() {
        Level++;
        CurrentXP = 0;
        MaxUnitCount++;
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
