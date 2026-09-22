using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 50f;
    private float UpLim = 80f;
    private float LowLim = 1f;
    private InputController _inputController;

    private Transform target; // Hacer privado y accesible mediante un método
    public Vector3 offset; // Desplazamiento de la cámara respecto al objetivo
    public float followSpeed = 10f; // Velocidad de seguimiento

    private void Awake()
    {
        _inputController = GetComponent<InputController>();
    }

    private void Start()
    {
        SetTargetWithTagPlayer(); // Buscar automáticamente un target con el tag "Player" al inicio
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            MouseCamera(); // Llamamos a MouseCamera() en LateUpdate para que la cámara siga al target y se apliquen los controles de ratón
        }
    }

    private void MouseCamera()
    {
        Vector2 input = _inputController.MouseInput();
        Vector3 angle = transform.rotation.eulerAngles;

        float _rotationX = angle.x - input.y * _mouseSensitivity * Time.deltaTime;
        float _rotationY = angle.y + input.x * _mouseSensitivity * Time.deltaTime;

        _rotationX = Mathf.Clamp(_rotationX, LowLim, UpLim);
        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, angle.z);
    }

    // Método para cambiar el target a un objeto con el tag "Player"
    public void SetTargetWithTagPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            target = players[0].transform; // Tomamos el primer objeto con el tag "Player"
        }
        else
        {
            Debug.LogWarning("No se encontraron objetos con el tag 'Player' en la escena.");
        }
    }

    // Método público para establecer el target manualmente si es necesario
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Método para obtener el target actual (útil para el CharacterController)
    public Transform GetTarget()
    {
        return target;
    }
}


