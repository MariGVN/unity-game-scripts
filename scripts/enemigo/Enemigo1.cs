using UnityEngine;

public class Enemigo1 : MonoBehaviour
{
    public float rangoDePersecucion = 500f;
    public float rangoDeAtaque = 2f;
    public float velocidadMovimiento = 5f;
    public Animator ani;
    private GameObject[] targets; // Array de targets (jugadores)
    private GameObject target; // Target actual del enemigo
    private bool atacando = false;
    private Vector3 randomDirection;
    private float changeDirectionTime = 2f;
    private float directionTimer;
    public float damageAmount = 10f; // Cantidad de daño que el enemigo inflige

    void Start()
    {
        ani = GetComponent<Animator>();
        directionTimer = changeDirectionTime;
        randomDirection = GetRandomDirection();

        // Buscar todos los jugadores en la escena al inicio
        targets = GameObject.FindGameObjectsWithTag("Player");

        // Seleccionar un target al azar entre los jugadores encontrados
        if (targets.Length > 0)
        {
            target = targets[Random.Range(0, targets.Length)];
        }
        else
        {
            Debug.LogError("No se encontraron jugadores en la escena con la etiqueta 'Player'.");
        }
    }

    void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("El target es null en Enemigo1.");
            BuscarTarget(); // Intentar buscar el target nuevamente si es nulo
            return;
        }

        float distancia = Vector3.Distance(transform.position, target.transform.position);

        if (distancia > rangoDePersecucion)
        {
            ani.SetBool("Running", false);
            ani.SetBool("Hook", false);
            ani.SetBool("Dancing", true);

            directionTimer -= Time.deltaTime;
            if (directionTimer <= 0)
            {
                randomDirection = GetRandomDirection();
                directionTimer = changeDirectionTime;
            }
            transform.Translate(randomDirection * velocidadMovimiento * Time.deltaTime);

            atacando = false;
        }
        else
        {
            ani.SetBool("Dancing", false);

            if (distancia > rangoDeAtaque)
            {
                var direccion = (target.transform.position - transform.position).normalized;
                var rotation = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
                transform.position += transform.forward * velocidadMovimiento * Time.deltaTime;
                ani.SetBool("Running", true);
                ani.SetBool("Hook", false);
                atacando = false;
            }
            else
            {
                if (!atacando)
                {
                    ani.SetBool("Running", false);
                    ani.SetBool("Hook", true);
                    atacando = true;

                    // Aplicar daño al jugador
                    AplicarDanioAlJugador();
                }
            }
        }
    }

    void BuscarTarget()
    {
        // Buscar todos los jugadores en la escena
        targets = GameObject.FindGameObjectsWithTag("Player");

        // Seleccionar un jugador al azar de los encontrados
        if (targets.Length > 0)
        {
            target = targets[Random.Range(0, targets.Length)];
        }
        else
        {
            Debug.LogError("No se encontraron jugadores en la escena con la etiqueta 'Player'.");
        }
    }

    void AplicarDanioAlJugador()
    {
        // Obtener el componente de salud del jugador
        Codigo_Salud saludJugador = target.GetComponent<Codigo_Salud>();

        if (saludJugador != null)
        {
            // Llamar a la función RecibirDaño del jugador con el daño especificado
            saludJugador.RecibirDaño(damageAmount);
        }
    }

    public void RecibirGolpe()
    {
        // Destruir el enemigo
        Destroy(gameObject);
    }

    Vector3 GetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        return new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle)).normalized;
    }
}
