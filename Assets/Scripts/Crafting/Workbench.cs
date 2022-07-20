using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Workbench : BaseInteractable
{
    public int tier = 1;
    private ICraftable[] craftableItems;
    private PlayerInventory playerInv;
    private CraftingItems craftingRef;
    private GameObject uiCanvas;

    // Start is called before the first frame update
    new void Awake()
    {
        Setup();
        uiCanvas = gameObject.transform.GetChild(0).gameObject;
        playerInv = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerInventory>();
        craftingRef = playerInv.craftingRef;
        //TODO: populate craftable item list with exactly the right amount based on workbench tier
        craftableItems = new ICraftable[10];
        
        craftingRef.comp1.ingredientList.Add(craftingRef.ing1, 2);
        craftingRef.comp1.ingredientList.Add(craftingRef.ing2, 3);

        for(int i = 0; i < 10; i++)
        {
            craftableItems[i] = craftingRef.comp1;
        }
    }

    public void CraftItem(int itemToCraft)
    {
        int ingredientCount = 0;
        ICraftable craftedItem = craftableItems[itemToCraft];

        foreach (KeyValuePair<ICraftable, int> item in craftedItem.ingredientList)
        {
            if (playerInv.inventory.ContainsKey(item.Key))
            {
                if (playerInv.inventory[item.Key] >= item.Value)
                {
                    HelperFunctions.AddOrUpdate(playerInv.inventory, item.Key, -item.Value);
                    ingredientCount++;
                }
            }
        }

        if(ingredientCount == craftedItem.ingredientList.Count)
        {
            Debug.Log("Crafted " + craftedItem.name);
            HelperFunctions.AddOrUpdate(playerInv.inventory, craftedItem, 1);
            //Debug.Log("Inventory after: " + playerInv.PrintInventory());
        } else
        {
            Debug.Log("Missing ingredient(s)!");
        }
    }

    public ICraftable[] GetCraftableList()
    {
        return craftableItems;
    }

    public override void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!interacted)
            {
                Debug.Log("Interacted Workbench!");
                interacted = true;
                pInput.SwitchCurrentActionMap("UI");
                camMove.cameraEnabled = false;
                uiCanvas.SetActive(true);
            }
            else
            {
                Debug.Log("Left Workbench!");
                interacted = false;
                pInput.SwitchCurrentActionMap("Gameplay");
                camMove.cameraEnabled = true;
                uiCanvas.SetActive(false);
            }
        }

        GetComponent<WorkbenchUI>().Initialize();

        HelperFunctions.ToggleCursor();
    }
}
