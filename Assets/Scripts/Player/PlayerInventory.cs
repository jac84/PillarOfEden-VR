using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    /*References to items*/
    [SerializeField] private Item Ether;
    [SerializeField] private Item Potion;

    [SerializeField] private List<Item> items;
    
    [SerializeField] private Transform itemSpawnHandLeft;
    [SerializeField] private Transform itemSpawnHandRight;
    [SerializeField] private Item currentItem;

    private bool switchingItem;
    private bool canOpenInv;
    private int index;

    // Use this for initialization
    void Start()
    {
        index = 0;
        if(items.Count >0)
            currentItem = items[index];
        canOpenInv = true;
    }
    public void AddPotion()
    {
        items.Add(Potion.GetComponent<Prototype>().Instantiate<Item>());
    }
    public void AddEther()
    {
        items.Add(Ether.GetComponent<Prototype>().Instantiate<Item>());
    }
    public void UseCurrentItem()
    {
        if (switchingItem)
            return;
        if (items.Count > 0)
        {
            switchingItem = true;
            items[index].UseItem();
            items[index].HideItemInInventory();
            items.RemoveAt(index);
            index = 0;
            Invoke("OpenInventory", 1f);
        }
            
    }
    public bool OpenInventory()
    {
        if (!canOpenInv)
            return false;
        if (items.Count > 0)
        {
            items[index].ShowItemInInventory(itemSpawnHandLeft);
            currentItem = items[index];
            switchingItem = false;
            return true;
        }
        return false;
    }
    public void CloseInventory()
    {
        if (items.Count > 0)
            items[index].HideItemInInventory();
        StartCoroutine("SetClosingInventory");

    }
    private IEnumerator SetClosingInventory()
    { 
        canOpenInv= false;
        yield return new WaitForSeconds(2.0f);
        canOpenInv = true;

    }
    public void NextItemInInventory()
    {
        if (items.Count > 0)
        {
            if (switchingItem)
                return;
            switchingItem = true;
            items[index].HideItemInInventory();
            if (index == items.Count - 1)
                index = 0;
            else
                index++;
            Debug.Log("NextItem In Inventory");
            currentItem = items[index];
            Invoke("OpenInventory", 1f);
        }
    }
    public void PreviousItemInInventory()
    {
        if (items.Count > 0)
        {
            if (switchingItem)
                return;
            switchingItem = true;
            items[index].HideItemInInventory();
            if (index != 0)
                index--;
            else
                index = items.Count - 1;
            Debug.Log("Previous In Inventory");
            currentItem = items[index];
            Invoke("OpenInventory", 1f);
        }
    }
    public int GetItemCount()
    {
        return items.Count;
    }
}
