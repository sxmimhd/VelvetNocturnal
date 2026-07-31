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
    public bool HasItem(InventoryItem item)
    {
        return items.Contains(item);
    }
    public List<string> GetItemIDs()
    {
        List<string> ids = new();

        foreach (InventoryItem item in items)
            ids.Add(item.name);

        return ids;
    }
    public void Restore(List<string> ids)
    {
        items.Clear();

        foreach (string id in ids)
        {
            InventoryItem item =
                Resources.Load<InventoryItem>("Inventory/" + id);

            if (item != null)
                items.Add(item);
        }

        InventoryUI.Instance.Refresh();
    }
}