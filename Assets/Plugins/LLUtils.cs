// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= //
//                 LibLab Utils                  //
//                                               //
//            Credit : Clement Roth              //
// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= //

using System.Collections.Generic;
using UnityEngine;

public static class LLUtils
{
    private static Vector3 _pos;

    public static void X(this Transform transform, float x)
    {
        _pos = transform.position;
        _pos.x = x;
        transform.position = _pos;
    }

    public static void Y(this Transform transform, float y)
    {
        _pos = transform.position;
        _pos.y = y;
        transform.position = _pos;
    }

    public static void Z(this Transform transform, float z)
    {
        _pos = transform.position;
        _pos.z = z;
        transform.position = _pos;
    }

    public static void LocalX(this Transform transform, float x)
    {
        _pos = transform.localPosition;
        _pos.x = x;
        transform.localPosition = _pos;
    }

    public static void LocalY(this Transform transform, float y)
    {
        _pos = transform.localPosition;
        _pos.y = y;
        transform.localPosition = _pos;
    }

    public static void LocalZ(this Transform transform, float z)
    {
        _pos = transform.localPosition;
        _pos.z = z;
        transform.localPosition = _pos;
    }

    public static float X(this Transform t)
    {
        return t.position.x;
    }

    public static float Y(this Transform t)
    {
        return t.position.y;
    }

    public static float Z(this Transform t)
    {
        return t.position.z;
    }

    public static float LocalX(this Transform t)
    {
        return t.localPosition.x;
    }

    public static float LocalY(this Transform t)
    {
        return t.localPosition.y;
    }

    public static float LocalZ(this Transform t)
    {
        return t.localPosition.z;
    }

    public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }

    public static Transform LastChild(this Transform t)
    {
        return t.GetChild(t.childCount - 1);
    }

    public static void DetachAllChild(this Transform t, Transform newParent = null, bool worldPositionStays = true)
    {
        for (int i = t.childCount - 1; i > -1; --i)
        {
            t.GetChild(i).SetParent(newParent, worldPositionStays);
        }
    }

    public static void FindGameObjectsWithLayer(this GameObject[] go, int layer)
    {
        var goArray = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new System.Collections.Generic.List<GameObject>();

        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }

        go = goList.ToArray();
    }

    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}