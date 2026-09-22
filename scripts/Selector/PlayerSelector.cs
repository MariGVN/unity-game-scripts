using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour
{
    public static event UnityAction<GameObject> OnJugadorSeleccionado; // Evento estático para la selección de jugador

    public Image[] characterImages; // Referencia a las imágenes de los personajes
    public GameObject[] characterPrefabs; // Referencia a los prefabs de los personajes

    void Start()
    {
        foreach (var img in characterImages)
        {
            img.gameObject.SetActive(false);
        }
        Select(0); // Selecciona el primer personaje por defecto
    }

    public void Select(int index)
    {
        foreach (var img in characterImages)
        {
            img.gameObject.SetActive(false);
        }
        characterImages[index].gameObject.SetActive(true);
        GameObject playerPrefab = characterPrefabs[index];
        PlayerStorage.playerPrefab = playerPrefab;

        // Disparar el evento de selección de jugador
        if (OnJugadorSeleccionado != null)
        {
            OnJugadorSeleccionado.Invoke(playerPrefab);
        }

        Debug.Log("Personaje seleccionado: " + playerPrefab.name);
    }
}

