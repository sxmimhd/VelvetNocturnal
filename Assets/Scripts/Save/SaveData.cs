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
    public bool CarlosUnlocked;

    public bool EnemyActivated;
    public string EnemyScene;
    public string EnemySpawnID;
    public EnemyManager.EnemyState EnemyState;
}