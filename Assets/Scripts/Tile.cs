using UnityEngine;
using UnityEditor;

public class Tile
{

    public GameObject blankTile;
    public GameObject[] possibleStates;
    public float[] normalizedWeights;
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

        // Initialize normalizedWeights Array
        normalizedWeights = new float[possibleStates.Length];
        NormalizeWeights();
    }

    public void NormalizeWeights()
    {
        float weight_sum = 0f;
        float[] nonNormalizedWeights = new float[possibleStates.Length];

        // Get all weights
        for (int i = 0; i < possibleStates.Length; i++)
        {
            float currentWeight = possibleStates[i].GetComponent<TileProperties>().weight;

            weight_sum += currentWeight;
            nonNormalizedWeights[i] = currentWeight;
        }

        float weightSummation = 0;

        // Normalize Weights
        for (int i = 0; i < possibleStates.Length; i++)
        {
            weightSummation += (nonNormalizedWeights[i] / weight_sum);
            normalizedWeights[i] = weightSummation;
        }
    }
}
