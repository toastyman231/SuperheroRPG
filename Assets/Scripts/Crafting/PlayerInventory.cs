using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<ICraftable, int> inventory;
    public CraftingItems craftingRef;

    // Start is called before the first frame update
    void Awake()
    {
        craftingRef = new CraftingItems();
        inventory = new Dictionary<ICraftable, int>();
        AddItem(craftingRef.ing1, 2);
        AddItem(craftingRef.ing2, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ICraftable AddItem(ICraftable itemToAdd, int numToAdd)
    {
        inventory.Add(itemToAdd, numToAdd);
        return itemToAdd;
    }

    public string PrintInventory()
    {
        string inv = "";

        foreach(KeyValuePair<ICraftable, int> item in inventory)
        {
            inv += item.Key.name + ": " + item.Value + "\n";
        }

        return inv;
    }
}
