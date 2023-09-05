using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tile
{

    public GameObject blankTile;
    public GameObject[] possibleStates;
    public bool hasCollapsed = false;

    public Tile()
    {
        // Load in all possibleStates
        string[] guids = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/PreFabs/Tiles" });

        possibleStates = new GameObject[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            possibleStates[i] = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        }
    
    }

}
