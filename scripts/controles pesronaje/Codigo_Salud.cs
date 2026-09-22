using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Importa la biblioteca de Scene Management

public class Codigo_Salud : MonoBehaviour
{
    public float Salud = 100;
    private float saludMaxima = 100; // Variable para la salud máxima

    private Image fondoSalud; // Fondo de la barra de salud
    private Image barraSalud; // Barra de salud
    private Text textoSalud; // Texto de la salud

    void Start()
    {
        // Encontrar automáticamente los elementos dentro del Canvas llamado "Salud"
        Transform saludCanvas = GameObject.Find("Salud").transform;
        fondoSalud = saludCanvas.Find("Fondo").GetComponent<Image>();
        barraSalud = saludCanvas.Find("Barra").GetComponent<Image>();
        textoSalud = saludCanvas.Find("TextoSalud").GetComponent<Text>();

        // Asegurarse de que los elementos fueron encontrados correctamente
        if (fondoSalud == null || barraSalud == null || textoSalud == null)
        {
            Debug.LogError("No se encontraron todos los elementos de interfaz en Codigo_Salud.");
        }
    }

    void Update()
    {
        ActualizarInterfaz();
    }

    public void RecibirDaño(float daño)
    {
        Salud -= daño;

        if (Salud <= 0)
        {
            Morir();
        }
    }

    public void RecuperarSalud(float cantidad)
    {
        Salud += cantidad;
        if (Salud > saludMaxima)
        {
            Salud = saludMaxima;
        }
        ActualizarInterfaz();
    }

    private void ActualizarInterfaz()
    {
        if (fondoSalud != null && barraSalud != null && textoSalud != null)
        {
            barraSalud.fillAmount = Salud / saludMaxima;
            textoSalud.text = "Salud: " + Salud.ToString("f0");
        }
    }

    private void Morir()
    {
        // Cargar la escena "Muerto" en lugar de mostrar el CanvasMuerto
        SceneManager.LoadScene("Muerto");
    }

    public void SetSaludMaxima(float maxSalud)
    {
        saludMaxima = maxSalud;
        Salud = maxSalud;
    }
}
