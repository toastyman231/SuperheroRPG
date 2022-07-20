using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunctions
{
    public static void AddOrUpdate(Dictionary<ICraftable, int> dict, ICraftable key, int value)
    {
        int val; 
        if (dict.TryGetValue(key, out val))
        {
            // yay, value exists!
            dict[key] = val + value;
        }
        else
        {
            // darn, lets add the value
            dict.Add(key, value);
        }
    }

    public static void ToggleCursor()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        Cursor.visible = !Cursor.visible;
    }
}
