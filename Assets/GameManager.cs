using UnityEngine;

public class GameManager : MonoBehaviour {
    public Player playerPrefab; // Assign the comprehensive player prefab in the Inspector

    private void Start() {
        SpawnPlayer(1, "Cam");
        SpawnPlayer(2, "Dog");
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
        Vector3 boardPosition = new Vector3(playerId * 10f, 0f, 0f); // Just an example
        player.transform.position = boardPosition;

        // Adjust the camera and other components as necessary
    }
}
