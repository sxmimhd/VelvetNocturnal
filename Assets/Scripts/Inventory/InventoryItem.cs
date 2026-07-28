using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Velvet Nocturnal/Inventory Item")]
public class InventoryItem : ScriptableObject
{
    [Header("General")]
    public string itemName;

    [TextArea(3, 6)]
    public string description;

    [Header("Images")]
    public Sprite icon;
    public Sprite largeImage;
}