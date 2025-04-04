using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void InventoryUpdateDelegate();
    public InventoryUpdateDelegate ItemRemoved;
    public InventoryUpdateDelegate ItemAdded;
    public InventoryUpdateDelegate ItemUpdated;

    Dictionary<int, int> _items = new()
    {
        {1,8},
        {5,10},
        {2,6},
    };

    public Dictionary<int, int> Items { get => _items; set => _items = value; }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    RemoveItem();
        //}
    }

    public void RemoveItem(int id, int amount)
    {
        if (Items.ContainsKey(id))
        {
            Items[id] -= amount;
            if (Items[id] <= 0)
            {
                Items.Remove(id);
                ItemRemoved?.Invoke();
            }
            else
            {
                ItemUpdated?.Invoke();
            }
        }
        //_items
    }


    public void AddItem(int id, int amount)
    {
        if (Items.ContainsKey(id))
        {
            Items[id] = amount;
            ItemUpdated?.Invoke();

        }

        else
        {
            Items.Add(id, amount);
            ItemAdded?.Invoke();
        }

        ShowInventory();
    }

    public void ShowInventory()
    {
        foreach (var item in Items)
        {
            Debug.Log("Id" + item.Key + " cantidad " + item.Value);
        }
    }
}
