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

    private void Start()
    {
        ShowItems();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.K))
    //    {
    //       ShowItems();

    //    }
    //}

    public void ShowItems()
    {
        foreach(var item in inventory.Items)
        {
            ItemDataSO currentItemData = itemsDatabase.SearchById(item.Key);
            GameObject currenButton = Instantiate(inventoryButton, content);
            currenButton.transform.Find("Icon").GetComponent<Image>().sprite = currentItemData.Icon; 
            currenButton.transform.Find("Image/Text (TMP)").GetComponent<TextMeshProUGUI>().text = item.Value.ToString();
            currenButton.GetComponent<Button>().onClick.AddListener(delegate
            {
                ShowSelectedItem(currentItemData, item.Value);
            });

        }
    }

    public void ShowSelectedItem(ItemDataSO itemdata, int amount )
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

    public void DelateInventoryItem(int id)
    {

    }
}
