using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities {

    public static float minViewPortDimension = -0.05f;
    public static float maxViewPortDimension = 1.05f;
    public static float viewPortChangeIncrement = 0.05f;


    public static bool isObjectOnScreen(Transform transform)
    {
        bool res = false;

        Vector2 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        res = (screenPoint.x > minViewPortDimension && screenPoint.x < maxViewPortDimension) && (screenPoint.y < maxViewPortDimension && screenPoint.y > minViewPortDimension);

        return res;
    }

    public static Vector2 trimViewportVector(Vector2 v) {
        Vector2 res = v;

        if(res.x < minViewPortDimension) {
            res = new Vector2(minViewPortDimension, res.y);
        } else if(res.x > maxViewPortDimension) {
            res = new Vector2(maxViewPortDimension, res.y);
        }

        if(res.y < minViewPortDimension) {
            res = new Vector2(res.x, minViewPortDimension);
        } else if(res.y > maxViewPortDimension) {
            res = new Vector2(res.x, maxViewPortDimension);
        }

        return res;
    }

    public static Vector2 translateViewportVectorToOppositeSide(Vector2 v) {
        return new Vector2(maxViewPortDimension + viewPortChangeIncrement - (v.x + viewPortChangeIncrement), maxViewPortDimension - (v.y + viewPortChangeIncrement));
    }
    
}
