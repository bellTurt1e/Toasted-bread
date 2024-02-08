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


}
