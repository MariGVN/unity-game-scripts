using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _speed = 30f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform _cameraMouse;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float attackRange = 2f; // Rango de ataque del jugador

    private InputController _inputController;
    private Rigidbody _rb;
    private bool isAttacking = false;
    private bool inventoryEnabled = false;
    private GameObject inventory;

    private void Awake()
    {
        _inputController = GetComponent<InputController>();
        _rb = GetComponent<Rigidbody>();

        // Asignar automáticamente _cameraMouse si no está asignado desde el inspector
        if (_cameraMouse == null)
        {
            _cameraMouse = Camera.main.transform;
        }

        inventory = GameObject.FindGameObjectWithTag("Inventory");
        if (inventory == null)
        {
            Debug.LogError("No se encontró ningún objeto con el tag 'Inventory'.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
            if (inventory != null)
            {
                inventory.SetActive(inventoryEnabled);
            }
        }

        if (!inventoryEnabled)
        {
            Move();

            if (_inputController.JumpInput())
            {
                Jump();
            }

            if (_inputController.AttackInput() && !isAttacking)
            {
                Attack();
            }

            // Cambia aquí para que la tecla E se use para recoger ítems
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryPickUpItem();
            }

            HandleAnimations();
        }
    }

    private void Move()
    {
        Vector2 input = _inputController.MoveInput();

        if (input != Vector2.zero)
        {
            // Dirección de movimiento relativa a la cámara
            Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized;

            // Transformar dirección al espacio de la cámara
            moveDirection = Quaternion.Euler(0, _cameraMouse.eulerAngles.y, 0) * moveDirection;

            // Aplicar movimiento
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.position += moveDirection * _speed * Time.deltaTime;

            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    private void Jump()
    {
        // Saltar solo si el personaje está en el suelo
        if (IsGrounded())
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        // Método para verificar si el personaje está en el suelo usando un raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            return true;
        }
        return false;
    }

    private void HandleAnimations()
    {
        // Manejo de animaciones basadas en input de teclado
        if (Input.GetKey("f"))
        {
            // Dejar de correr y comenzar a bailar
            animator.SetBool("Running", false);
            animator.Play("Dancing");
        }
        else if (Input.GetKey("g"))
        {
            // Dejar de correr y ejecutar Hook
            animator.SetBool("Running", false);
            animator.Play("Hook");
            isAttacking = true; // Indicamos que estamos atacando
        }
        else if (Input.GetKey("r"))
        {
            // Dejar de bailar y de ejecutar Hook, y comenzar a correr
            animator.SetBool("Dancing", false);
            animator.SetBool("Hook", false);
            animator.Play("Running");
        }
    }

    private void Attack()
    {
        // Esperar un pequeño retraso antes de aplicar el daño para sincronizar con la animación
        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(0.5f); // Ajustar el retraso según la animación

        // Verificar si hay enemigos en el rango de ataque
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var hitCollider in hitColliders)
        {
            Enemigo1 enemigo = hitCollider.GetComponent<Enemigo1>();
            if (enemigo != null)
            {
                enemigo.RecibirGolpe();
            }
        }
        isAttacking = false; // Resetear el estado de ataque
    }

    private void TryPickUpItem()
    {
        // Detectar colliders en un radio pequeño alrededor del personaje
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f); // Ajusta el rango de recogida
        Debug.Log("Items encontrados: " + hitColliders.Length);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Item"))
            {
                GameObject item = hitCollider.gameObject;

                // Llamar al método AddItem del Inventory para agregar el ítem al inventario
                Inventory inventoryComponent = inventory.GetComponent<Inventory>();
                if (inventoryComponent != null)
                {
                    Item itemComponent = item.GetComponent<Item>();
                    if (itemComponent != null)
                    {
                        inventoryComponent.AddItem(item, itemComponent.ID, itemComponent.type, itemComponent.descripcion, itemComponent.icon);
                    }
                }

                // Destruir el ítem del mundo después de recogerlo
                Destroy(item);

                break; // Salir del bucle después de recoger un ítem
            }
        }
    }
}
