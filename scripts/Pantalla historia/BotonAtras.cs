using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonAtras : MonoBehaviour
{
        public void ChangeScene()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
