using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] private InventorySlot[] slots;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image previewImage;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    private bool isOpen;
    private int selectedIndex = 0;
    private void Start()
    {
        panel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;

            panel.SetActive(isOpen);

            if (isOpen)
                selectedIndex = 0;
                RefreshPreview();
        }

        if (!isOpen)
            return;

        if (Input.GetKeyDown(KeyCode.D))
        {
            selectedIndex++;

            if (selectedIndex >= slots.Length)
                selectedIndex = 0;

            RefreshPreview();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            selectedIndex--;

            if (selectedIndex < 0)
                selectedIndex = slots.Length - 1;

            RefreshPreview();
        }
 
    }
    private void RefreshPreview()
    {
        if (selectedIndex >= InventoryManager.Instance.Items.Count)
        {
            previewImage.enabled = false;
            titleText.text = "";
            descriptionText.text = "";
            return;
        }

        InventoryItem item = InventoryManager.Instance.Items[selectedIndex];

        previewImage.enabled = true;
        previewImage.sprite = item.largeImage;
        titleText.text = item.itemName;
        descriptionText.text = item.description;
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Refresh()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < InventoryManager.Instance.Items.Count)
                slots[i].SetItem(InventoryManager.Instance.Items[i]);
            else
                slots[i].SetItem(null);
        }
    }
}