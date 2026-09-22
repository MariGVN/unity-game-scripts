using UnityEngine;

public class ItemCuracion : MonoBehaviour
{
    public float cantidadCuracion = 20f; // Cantidad de salud que restaura el ítem

    private void OnTriggerEnter(Collider other)
    {
        Codigo_Salud salud = other.GetComponent<Codigo_Salud>();
        if (salud != null)
        {
            salud.RecuperarSalud(cantidadCuracion);
            Destroy(gameObject); // Destruir el ítem de curación después de usarlo
        }
    }
}
