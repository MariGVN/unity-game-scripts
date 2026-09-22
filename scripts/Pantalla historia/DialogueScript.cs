using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueScript : MonoBehaviour
{
    public Text dialogueText;
    public string[] lines;
    public float textSpeed = 0.1f;
    int index;

    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue(); 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
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
        dialogueText.text = string.Empty; // Reinicia el texto antes de escribir la nueva línea
        foreach (char letter in lines[index].ToCharArray())
        {   
            dialogueText.text += letter;
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

    public void ChangeScene()
    {
        SceneManager.LoadScene("MenuPersonajes");
    }
}
