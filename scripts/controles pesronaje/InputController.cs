using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Vector2 MoveInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        return new Vector2(x, y);
    }

    public Vector2 MouseInput()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        return new Vector2(x, y);
    }

    public bool JumpInput()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool AttackInput()
    {
        return Input.GetKeyDown(KeyCode.G); // Cambiado para detectar la tecla G para el ataque
    }
}
