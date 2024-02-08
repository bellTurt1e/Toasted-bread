using UnityEngine;

public class GameManager : MonoBehaviour {
    public Player playerPrefab; // Assign the comprehensive player prefab in the Inspector
    public float xValue = 0;
    public ArenaCameraSwitcher cameraSwitcher; // Assign this in the Inspector



    [SerializeField] int playerIdCounter = 0;

    private void Start() {
        SpawnPlayer("Cam");
        SpawnPlayer("ray");
        cameraSwitcher.SetupCameras();
    }

    public void SpawnPlayer(string playerName) {
        Player newPlayer = Instantiate(playerPrefab);

        // Setup the player
        newPlayer.setupPlayer(playerName, playerIdCounter); // create a new player and set the player name and id
        PositionPlayerBoard(newPlayer); // position board starting at 0,0,0 then placing the next one 100 on the x distance away
        playerIdCounter++;
    }

    void PositionPlayerBoard(Player player) {  
        Vector3 boardPosition = new Vector3(xValue, 0, 0);
        player.transform.position = boardPosition;
        xValue += 100;
    }
}
