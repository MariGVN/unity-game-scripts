using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject item;
    public int ID;
    public string type;
    public string description;
    public bool empty = true;
    public Sprite icon;

    private Image slotIconImage;

    private void Start()
    {
        slotIconImage = GetComponent<Image>(); // Obtener el componente Image del slot mismo
        UpdateSlot();
    }

    public void AddItem(GameObject newItem, int newItemID, string newItemType, string newItemDescription, Sprite newItemIcon)
    {
        item = newItem;
        ID = newItemID;
        type = newItemType;
        description = newItemDescription;
        icon = newItemIcon;

        item.transform.SetParent(transform); // Establecer el ítem como hijo de este slot
        item.SetActive(false); // Desactivar el ítem para que no sea visible en la escena

        empty = false;
        UpdateSlot(); // Actualizar el slot después de agregar el ítem
    }

    public void UpdateSlot()
    {
        if (!empty && item != null)
        {
            slotIconImage.sprite = icon; // Asignar el ícono del ítem al componente Image del slot
            slotIconImage.enabled = true; // Asegurar que la imagen esté activada
        }
        else
        {
            slotIconImage.sprite = null; // Limpiar el ícono si no hay ítem
            slotIconImage.enabled = false; // Desactivar la imagen si no hay ítem
        }
    }
}
