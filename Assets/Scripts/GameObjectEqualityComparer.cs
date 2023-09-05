using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameObjectEqualityComparer : IEqualityComparer<GameObject>
{
    public bool Equals(GameObject x, GameObject y)
    {
        return x.name == y.name;
    }

    public int GetHashCode(GameObject obj)
    {
        return obj.name.GetHashCode();
    }
}
