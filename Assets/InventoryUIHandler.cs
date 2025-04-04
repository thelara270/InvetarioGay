using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHandler : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] ItemsDatabaseSO itemsDatabase;

    [SerializeField] GameObject inventoryButton;
    [SerializeField] Transform content;

    [SerializeField] Image previewItemImage;
    [SerializeField] TextMeshProUGUI previewItemNameText;
    [SerializeField] TextMeshProUGUI previewItemAmountText;

    [SerializeField] CanvasGroup panelItemPreview;

    List<GameObject> instantiateButtons = new List<GameObject>();

    int selectdItemId;

    private void Start()
    {
        //ShowItems();
        IntantiateButtons();
        SetInventory(inventory);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            ShowItems();

        }
    }

    public void SetInventory(Inventory newInventory)
    {
        inventory = newInventory;
        inventory.ItemUpdated += ShowItems;
        inventory.ItemRemoved += ShowItems;
        inventory.ItemAdded += ShowItems;
       
        inventory.ItemUpdated += UpdatePreviewAmountText;
        inventory.ItemRemoved += HidePreviewPanel;
    }

    public void IntantiateButtons()
    {
        for (int i = 0; i < itemsDatabase.Items.Count; i++)
        {
            GameObject currenButton = Instantiate(inventoryButton, content);
            currenButton.SetActive(false);
            instantiateButtons.Add(currenButton);
        }
    }


    public void ShowItems()
    {
        for (int i = 0; i < itemsDatabase.Items.Count; i++)
        {
            instantiateButtons[i].SetActive(false);
        }

        foreach (var item in inventory.Items)
        {
            ItemDataSO currentItemData = itemsDatabase.SearchById(item.Key);
            GameObject currenButton = instantiateButtons.Find(x => x.activeSelf == false);
            currenButton.SetActive(true);
            currenButton.transform.Find("Icon").GetComponent<Image>().sprite = currentItemData.Icon;
            currenButton.transform.Find("Image/Text (TMP)").GetComponent<TextMeshProUGUI>().text = item.Value.ToString();
            currenButton.GetComponent<Button>().onClick.AddListener(delegate
            {
                ShowSelectedItem(currentItemData, item.Value);
                selectdItemId = item.Key;
            });

        }
    }

    public void ShowSelectedItem(ItemDataSO itemdata, int amount)
    {
        ShowPreviewPanel();

        previewItemImage.sprite = itemdata.Icon;
        previewItemNameText.text = itemdata.ItemName;
        previewItemAmountText.text = amount.ToString();
    }

    public void ShowPreviewPanel()
    {
        panelItemPreview.alpha = 1;
        panelItemPreview.interactable = true;
        panelItemPreview.blocksRaycasts = true;
    }

    public void HidePreviewPanel()
    {
        panelItemPreview.alpha = 0;
        panelItemPreview.interactable = false;
        panelItemPreview.blocksRaycasts = false;
    }

    public void DelateInventoryItem(int id)
    {
        inventory.RemoveItem(selectdItemId, 1);
    }

    public void UpdatePreviewAmountText()
    {
        previewItemAmountText.text = inventory.Items[selectdItemId].ToString();
    }
}