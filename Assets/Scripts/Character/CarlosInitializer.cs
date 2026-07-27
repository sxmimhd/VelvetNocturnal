using UnityEngine;

public class PrisonInitializer : MonoBehaviour
{
    private void Start()
    {
        CharacterManager.Instance.UnlockCarlos();
    }
}