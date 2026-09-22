using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonSalir : MonoBehaviour
{
    public void SaliR()
    {
        Application.Quit();
        Debug.Log("Se ha salido del juego");
    }
}
