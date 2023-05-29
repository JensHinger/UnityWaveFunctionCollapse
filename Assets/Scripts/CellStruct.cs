using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CellStruct
{
    public GameObject[] PossibleStates { get; }
    public GameObject CollapsedState { get;  set; }

    public CellStruct(GameObject[] possibleStates)
    {
        PossibleStates = possibleStates;
        CollapsedState = null;
    }
}
