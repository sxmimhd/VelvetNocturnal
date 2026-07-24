using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Velvet Nocturnal/Dialogue")]
public class Dialogue : ScriptableObject
{
    [Header("Speaker")]
    public string speakerName;

    public Sprite portrait;

    [Header("Dialogue")]
    [TextArea(3, 5)]
    public string[] lines;
}