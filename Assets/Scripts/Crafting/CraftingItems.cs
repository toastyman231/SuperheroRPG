using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingItems
{
    public Component ing1;
    public Component ing2;
    public Component comp1;

    public CraftingItems()
    {
        ing1 = new Component("Ing 1", "First ing");
        ing2 = new Component("Ing 2", "Second ing");
        comp1 = new Component("Comp 1", "First Comp");
    }
}
