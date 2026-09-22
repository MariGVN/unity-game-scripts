using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 spawnPosition = new Vector3(8050.2f, 153.2f, 8472.98f);
    public Vector3 spawnRotation = Vector3.zero;
    public Vector3 spawnScale = new Vector3(0.433f, 0.363f, 0.419f);

    void Start()
    {
        if (PlayerStorage.playerPrefab != null)
        {
            GameObject player = Instantiate(PlayerStorage.playerPrefab, spawnPosition, Quaternion.Euler(spawnRotation));
            player.transform.localScale = spawnScale;
            player.tag = "Player"; // Asegurarnos de que el jugador tenga el tag "Player"

            CameraController cameraController = Camera.main.GetComponent<CameraController>();
            if (cameraController != null)
            {
                cameraController.SetTarget(player.transform); // Establecemos el target de la cámara al jugador instanciado
            }
        }
        else
        {
            Debug.LogError("PlayerPrefab no asignado en PlayerSpawner.");
        }

        Destroy(gameObject);
    }
}
