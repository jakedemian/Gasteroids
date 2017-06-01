using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities {

    public static bool isObjectOnScreen(Transform transform)
    {
        bool res = false;

        Vector2 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        res = (screenPoint.x > 0 && screenPoint.x < 1) && (screenPoint.y < 1 && screenPoint.y > 0);

        return res;
    }
    
}
