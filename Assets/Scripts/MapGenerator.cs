using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int MapSize;
    [SerializeField] private GameObject[] PossibleStates;
    private CellStruct[,] Grid;

    // Start is called before the first frame update
    void Start()
    {
        Grid = new CellStruct[MapSize, MapSize];

        // Create grid world based on map_size
        for (int x = 0; x < MapSize; x++)
        { 
            for (int y = 0; y < MapSize; y++)
            {
                Grid[x, y] = new CellStruct(PossibleStates);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    void CollapseRandomCell()
    {
        int XCoordinate = Random.Range(0, MapSize);
        int YCoordinate = Random.Range(0, MapSize);

        int RandomState = Random.Range(0, PossibleStates.Length);
        Grid[XCoordinate, YCoordinate].CollapsedState = Grid[XCoordinate, YCoordinate].PossibleStates[RandomState];
    }

    void PropogateCollapse(int XCoordinate, int YCoordinate)
    {
        // Should go through cell
        // Starting at the origin of "collapse"?
    }

    void EliminatePossibleStates()
    {
        // Needs to check the coordinates of its neighbouring cells
        // To make it faster if neighbouring cell has a PossibleStates field with length of all PossibleStates then skip?
    }
}
