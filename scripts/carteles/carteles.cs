using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class carteles : MonoBehaviour
{
    public GameObject informacionPanel; // Panel que contiene el texto
    public Text informacionText; // Componente de texto

    private bool informacionHabilitada = false;

    void Start()
    {
        informacionPanel.SetActive(false); // Oculta el panel al inicio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            informacionHabilitada = !informacionHabilitada;
            informacionPanel.SetActive(informacionHabilitada);
        }
    }

    public void SetInformacionText(string newText)
    {
        informacionText.text = newText;
    }
}
