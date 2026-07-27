using Unity.Cinemachine;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    [SerializeField] private PlayerMovement lumy;
    [SerializeField] private PlayerMovement carlos;

    [SerializeField] private CinemachineCamera cinemachineCamera;

    public PlayerMovement CurrentCharacter { get; private set; }
    public PlayerMovement Lumy => lumy;
    public PlayerMovement Carlos => carlos;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SetCurrentCharacter(lumy);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwitchCharacter();
        }
    }

    public void SwitchCharacter()
    {
        if (CurrentCharacter == lumy)
            SetCurrentCharacter(carlos);
        else
            SetCurrentCharacter(lumy);
    }

    private void SetCurrentCharacter(PlayerMovement player)
    {
        lumy.CanMove = false;
        carlos.CanMove = false;

        lumy.GetComponent<PlayerInteraction>().CanInteract = false;
        carlos.GetComponent<PlayerInteraction>().CanInteract = false;

        CurrentCharacter = player;

        CurrentCharacter.CanMove = true;
        CurrentCharacter.GetComponent<PlayerInteraction>().CanInteract = true;

        cinemachineCamera.Follow = CurrentCharacter.transform;
    }
}