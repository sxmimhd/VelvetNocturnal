using Unity.Cinemachine;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    [SerializeField] private PlayerMovement lumy;
    [SerializeField] private PlayerMovement carlos;
    [SerializeField] private bool carlosUnlocked;
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

        carlos.gameObject.SetActive(carlosUnlocked);
    }

    private void Update()
    {
        if (!carlosUnlocked)
            return;

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
        SpriteRenderer lumySprite = lumy.GetComponent<SpriteRenderer>();
        SpriteRenderer carlosSprite = carlos.GetComponent<SpriteRenderer>();

        if (CurrentCharacter == lumy)
        {
            lumySprite.sortingOrder = 2;
            carlosSprite.sortingOrder = 1;
        }
        else
        {
            carlosSprite.sortingOrder = 2;
            lumySprite.sortingOrder = 1;
        }
    }
    public void UnlockCarlos()
    {
        if (carlosUnlocked)
            return;

        carlosUnlocked = true;

        carlos.gameObject.SetActive(true);

        carlos.transform.position =
            lumy.transform.position + Vector3.right * 1.2f;
    }
}