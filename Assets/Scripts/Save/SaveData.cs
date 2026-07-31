using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    // Scene
    public string SceneName;
    public string SpawnID;

    // Inventory
    public List<string> InventoryItems = new();

    // Completed interactions
    public List<string> CompletedInteractions = new();

    // Metadata
    public string SaveDate;
    public float PlayTime;
}