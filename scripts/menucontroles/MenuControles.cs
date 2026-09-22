using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControles : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
