using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class GridFunctions {
    public static bool Vector2PassedPoint(Vector2 input, Vector2 point, Vector2 dir) {
        if (input.x * dir.x > point.x * dir.x || input.y * dir.y > point.y * dir.y) return true;
        else return false;
    }

    public static Vector3Int Vector2ToVector3Int(Vector2 v2) {
        return new Vector3Int(Mathf.RoundToInt(v2.x), Mathf.RoundToInt(v2.y), 0);
    }
}