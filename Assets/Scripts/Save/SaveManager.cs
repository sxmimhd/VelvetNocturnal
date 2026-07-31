using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private string GetPath()
    {
        return Path.Combine(
            Application.persistentDataPath,
            "save.json");
    }

    public bool SaveExists()
    {
        return File.Exists(GetPath());
    }

    public SaveData Load()
    {
        if (!File.Exists(GetPath()))
            return null;

        string json = File.ReadAllText(GetPath());

        return JsonUtility.FromJson<SaveData>(json);
    }

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(GetPath(), json);
    }

    public void Delete()
    {
        if (File.Exists(GetPath()))
            File.Delete(GetPath());
    }
    
    public void SaveGame(
        string scene,
        string spawnID)
    {
        SaveData data = new();

        data.SceneName = scene;
        data.SpawnID = spawnID;

        data.InventoryItems =
            InventoryManager.Instance.GetItemIDs();

        data.CompletedInteractions =
            InteractionManager.Instance.GetCompleted();

        data.SaveDate =
            System.DateTime.Now.ToString("dd/MM/yyyy HH:mm");

        Save(data);
    }
    public void LoadGame()
    {
        SaveData data = Load();

        if (data == null)
            return;

        InventoryManager.Instance.Restore(
            data.InventoryItems);

        InteractionManager.Instance.Restore(
            data.CompletedInteractions);

        SceneTransitionManager.Instance.LoadScene(
            data.SceneName,
            data.SpawnID,
            1f);
    }
    public void NewGame()
    {
        Delete();

        InventoryManager.Instance.Restore(new());

        InteractionManager.Instance.Restore(new());

        SceneTransitionManager.Instance.LoadScene(
            "cemetery",
            "start",
            2f);
    }
}