using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICraftable
{
    public string name { get; set; }
    public string description { get; set; }
    public Dictionary<ICraftable, int> ingredientList { get; set; }

    public static ICraftable Craft(ICraftable itemToCraft)
    {
        //TODO: Generate list of items at runtime and pass them into this function when crafting
        Debug.Log("Crafted " + itemToCraft.name);
        return itemToCraft;
    }
}

public class Component : ICraftable
{
    public string name { get; set; }
    public string description { get; set; }
    public Dictionary<ICraftable, int> ingredientList { get; set; }

    public Component()
    {
        name = "A Component";
        description = "This component is for testing!";
        ingredientList = new Dictionary<ICraftable, int>();
    }

    public Component(string compName, string desc)
    {
        name = compName;
        description = desc;
        ingredientList = new Dictionary<ICraftable, int>();
    }
}

public class Power : ICraftable
{
    public string name { get; set; }
    public string description { get; set; }
    public Dictionary<ICraftable, int> ingredientList { get; set; }

    public string animationTrigger;
    public ParticleSystem particle;

    public Power()
    {
        name = "A Power";
        description = "This power is for testing!";
        ingredientList = new Dictionary<ICraftable, int>();
        animationTrigger = "Punch";
    }

    public Power(string powerName, string desc, string trigger, ParticleSystem part)
    {
        name = powerName;
        description = desc;
        ingredientList = new Dictionary<ICraftable, int>();
        animationTrigger = trigger;
        particle = part;
    }
}
