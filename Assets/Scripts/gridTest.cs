using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class gridTest : MonoBehaviour
{

    public GameObject blankTile;
    public int fieldSize;

    private Tile[,] Grid;
    private GameObject[,] VisibleGrid;
    private bool collapsedFirst = false;

    // Start is called before the first frame update
    void Start()
    {
        Grid = new Tile[fieldSize, fieldSize];
        VisibleGrid = new GameObject[fieldSize, fieldSize];

        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                Grid[i, j] = new Tile();
            }
        }

        DrawGrid();

        StartCoroutine(collapseCell());
    }

    void DrawGrid()
    {
        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                // Get height width of tile -> expects all tiles to be the same size currently
                float height = blankTile.transform.localScale.y;
                float width = blankTile.transform.localScale.x;
                Vector3 spawnLoc = new Vector3(width * i, height * j);

                // Checks if the tile is collapsed
                // Maybe check if thing at point is same or not -> destroy 
                if (Grid[i, j].possibleStates.Length > 1)
                {
                    VisibleGrid[i,j] = GameObject.Instantiate(blankTile, spawnLoc, blankTile.transform.localRotation);
                } else
                {
                    VisibleGrid[i, j] = GameObject.Instantiate(Grid[i, j].possibleStates[0], spawnLoc, Grid[i, j].possibleStates[0].transform.localRotation);
                }
               
            }
        }
    }

    private IEnumerator collapseCell()
    {
        yield return new WaitForSeconds(.1f);

        int collapseX = 0;
        int collapseY = 0;

        // First Cell should be random
        if (!collapsedFirst) 
        { 
            collapseX = Random.Range(0, fieldSize);
            collapseY = Random.Range(0, fieldSize);
            collapsedFirst = true;
        }
        else
        {

            int lowestEntropy = 100000;

            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    int currentEntropy = Grid[i, j].possibleStates.Length;

                    if (currentEntropy < lowestEntropy && !Grid[i, j].hasCollapsed)
                    {
                        collapseX = i;
                        collapseY = j;
                        lowestEntropy = currentEntropy;
                    }
                }
            }
        }
        // After First Cell collapse by entropy

        int countPossStates = Grid[collapseX, collapseY].possibleStates.Length;
        int randomChoice = Random.Range(0, countPossStates);

        Grid[collapseX, collapseY].possibleStates = new GameObject[] { Grid[collapseX, collapseY].possibleStates[randomChoice] };
        Grid[collapseX, collapseY].hasCollapsed = true;

        Destroy(VisibleGrid[collapseX, collapseY]);
        VisibleGrid[collapseX, collapseY] = GameObject.Instantiate(Grid[collapseX, collapseY].possibleStates[0], new(10 * collapseX, 10 * collapseY), Grid[collapseX, collapseY].possibleStates[0].transform.localRotation);

        propogateCollapse(collapseX, collapseY);
    }

    public void propogateCollapse(int x, int y)
    {
        // Go through neigbours
        List<Tile> Neighbours = getNeighbours(x, y);

        // Get intersection of all possible states
        foreach (Tile neighbour in Neighbours)
            neighbour.possibleStates = Grid[x, y].possibleStates[0].GetComponent<TileProperties>().allowedTiles.Intersect(neighbour.possibleStates, new GameObjectEqualityComparer()).ToArray();

        StartCoroutine(collapseCell());
    }

    public List<Tile> getNeighbours(int x, int y)
    {
        List<Tile> Neighbours = new List<Tile>();

        Debug.Log(x + "   " + y);

        if (y + 1 < fieldSize) 
        {
            //UP
            Neighbours.Add(Grid[x, y + 1]);
        }

        if (x + 1 < fieldSize) 
        { 
            //RIGHT
            Neighbours.Add(Grid[x + 1, y]);
        }

        if (y - 1 >= 0)
        {
            //DOWN
            Neighbours.Add(Grid[x, y - 1]);
        }

        if (x - 1 >= 0)
        {
            //LEFT
            Neighbours.Add(Grid[x - 1, y]);
        }

        return Neighbours;
    }

    // Update is called once per frame
        void FixedUpdate()
    {
      
    }
}
