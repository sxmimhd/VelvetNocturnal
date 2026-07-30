using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;

    private readonly HashSet<string> completed = new();

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

    public bool IsCompleted(string id)
    {
        return completed.Contains(id);
    }

    public void Complete(string id)
    {
        completed.Add(id);
    }
}