using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private string playerName;
    [SerializeField] private int coins = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private int playerId;
    [SerializeField] private int currentXP = 0;
    [SerializeField] private int maxUnitCount = 1;
    [SerializeField] private List<Unit> units = new List<Unit>();
    [SerializeField] private List<int> xpRequirements = new List<int>();
    [SerializeField] private LevelData levelData;

    public int getPlayerId() {
        return playerId;
    }

    public void setupPlayer(string name, int id) {
        playerName = name;
        playerId = id;
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

    private void levelUp() {
        level++;
        currentXP = 0;
        maxUnitCount++;
    }

    public int getMaxUnitCount() {
        return maxUnitCount;
    }


    public void AddUnit(Unit unit) {
        if (units.Count < getMaxUnitCount()) {
            units.Add(unit);
            unit.teamId = playerId;
        }
        else {
            // Do nothing (for now)
        }
    }

}
