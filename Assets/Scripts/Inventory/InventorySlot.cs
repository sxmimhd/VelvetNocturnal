using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;


    public void SetItem(InventoryItem item)
    {
        if (item == null)
        {
            icon.enabled = false;
            return;
        }

        icon.enabled = true;
        icon.sprite = item.icon;
    }
}