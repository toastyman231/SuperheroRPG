using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkbenchUI : MonoBehaviour
{
    private GameObject content;
    public GameObject listElement;
    private Workbench bench;

    private void Start()
    {
        bench = GetComponent<Workbench>();
    }

    public void Initialize()
    {
        if(content == null)
        {
            content = GameObject.FindGameObjectWithTag("Content");
        }

        foreach(Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        int index = 0;
        foreach (ICraftable item in bench.GetCraftableList())
        {
            GameObject listItem = Instantiate(listElement, content.transform);
            TextMeshProUGUI itemText = listItem.GetComponentInChildren<TextMeshProUGUI>();
            Button itemButton = listItem.GetComponentInChildren<Button>();
            string recipe = "";
            itemText.text = recipe;

            int otherIndex = 0;
            foreach (KeyValuePair<ICraftable, int> comp in item.ingredientList)
            {
                recipe += comp.Key.name + ": " + comp.Value;

                if (otherIndex != item.ingredientList.Count - 1)
                {
                    recipe += " + ";
                }

                otherIndex++;
            }

            itemText.text = recipe;

            int x = index;
            itemButton.onClick.AddListener(delegate { bench.CraftItem(x); });
            index++;
        }
    }
}
