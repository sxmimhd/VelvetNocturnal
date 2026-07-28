using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private readonly List<InventoryItem> items = new();

    public IReadOnlyList<InventoryItem> Items => items;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(InventoryItem item)
    {
        if (item == null)
            return;

        items.Add(item);

        InventoryUI.Instance.Refresh();
    }
}