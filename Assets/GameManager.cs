using UnityEngine;

public class GameManager : MonoBehaviour {
    public Player playerPrefab; // Assign the comprehensive player prefab in the Inspector
    public float xValue = 0;

    private void Start() {
        SpawnPlayer(1, "Cam");
        SpawnPlayer(2, "ray");
    }

    public void SpawnPlayer(int playerId, string playerName) {
        // Instantiate the player prefab
        Player newPlayer = Instantiate(playerPrefab);

        // Setup the player
        newPlayer.SetupPlayer(playerName, playerId);

        // Additional setup as needed, such as positioning the player's board and camera
        PositionPlayerBoard(newPlayer, playerId);

        // Optionally, set up the player's shop UI
        //newPlayer.SetupShop();

        // Any other player-specific initialization can be done here
    }

    void PositionPlayerBoard(Player player, int playerId) {
        // Example positioning logic based on playerId
        
        Vector3 boardPosition = new Vector3(xValue, 100f, 100f); // Just an example
        player.transform.position = boardPosition;
        xValue += 100;

        // Adjust the camera and other components as necessary
    }
}
