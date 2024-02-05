using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game Data/Level Data")]
public class LevelData : ScriptableObject {
    public List<int> xpRequirements = new List<int>();
}
