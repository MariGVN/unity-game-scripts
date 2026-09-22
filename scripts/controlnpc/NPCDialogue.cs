using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public GameObject informacionPanel; // Panel que contiene el texto
    public Text informacionText; // Componente de texto
    public string[] lines; // Líneas de diálogo
    public float textSpeed = 0.1f; // Velocidad del texto
    private bool informacionHabilitada = false;
    private int index;

    void Start()
    {
        informacionPanel.SetActive(false); // Oculta el panel al inicio
        informacionText.text = string.Empty;
    }

    void Update()
    {
        // Mostrar/ocultar panel con la tecla H
        if (Input.GetKeyDown(KeyCode.H))
        {
            informacionHabilitada = !informacionHabilitada;
            informacionPanel.SetActive(informacionHabilitada);

            if (informacionHabilitada)
            {
                StartDialogue();
            }
            else
            {
                StopAllCoroutines();
                informacionText.text = string.Empty;
            }
        }

        // Control del diálogo con la tecla Enter
        if (Input.GetKeyDown(KeyCode.Return) && informacionHabilitada)
        {
            if (informacionText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                informacionText.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        informacionText.text = string.Empty; // Reinicia el texto antes de escribir la nueva línea
        foreach (char letter in lines[index].ToCharArray())
        {
            informacionText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(WriteLine());
        }
        else
        {
            Debug.Log("End of dialogue");
        }
    }
}
