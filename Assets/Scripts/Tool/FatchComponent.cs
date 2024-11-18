using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchComponent
{
    public static Component GetSpecificComponent<T>(GameObject gameObject)
    {
        Component[] componets = gameObject.GetComponents<Component>();
        foreach (Component cp in componets)
        {
            if (cp is T)
            {
                return cp;
            }
        }
        return null;
    }
}
