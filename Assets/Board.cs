using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] int boardId;

    public void setBoardId(int boardId) {
        this.boardId = boardId;
    }

}
