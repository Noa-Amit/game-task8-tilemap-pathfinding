using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

/**
 * This class demonstrates the CaveGenerator on a Tilemap.
 * 
 * By: Erel Segal-Halevi
 * Since: 2020-12
 */

public class NewTilemapCaveGenerator: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;

    [Tooltip("The tile that represents a wall (an impassable block)")]
    [SerializeField] TileBase wallTile = null;

    [Tooltip("The tile that represents a floor (a passable block)")]
    [SerializeField] TileBase floorTile = null;

    [Tooltip("The tile that represents a bush (a passable block)")]
    [SerializeField] TileBase bushTile = null;

    [Tooltip("The percent of walls in the initial random map")]
    [Range(0, 1)]
    [SerializeField] float randomFillPercentWall = 0.1f;

    [Tooltip("The percent of bushes in the initial random map")]
    [Range(0, 1)]
    [SerializeField] float randomFillPercentBush = 0.4f;

    [Tooltip("Length and height of the grid")]
    [SerializeField] int gridSize = 50;

    [Tooltip("How many steps do we want to simulate?")]
    [SerializeField] int simulationSteps = 20;

    // [Tooltip("For how long will we pause between each simulation step so we can look at the result?")]
    // [SerializeField] float pauseTime = 1f;

    private NewCaveGenerator caveGenerator;
    private float[] randomFillPercent;

    void Start() {
        initMap();
    }

    public void initMap () {
        Random.InitState(100);

        float randomFillPercentFloor = 1-(randomFillPercentWall+randomFillPercentBush);
        randomFillPercent = new float[] {randomFillPercentFloor,randomFillPercentWall,randomFillPercentBush};
        
        caveGenerator = new NewCaveGenerator(randomFillPercent, gridSize,1);
        caveGenerator.RandomizeMap();
                
        //For testing that init is working
        GenerateAndDisplayTexture(caveGenerator.GetMap());
            
        //Start the simulation
        StartCoroutine(SimulateCavePattern());
    }


    //Do the simulation in a coroutine so we can pause and see what's going on
    private IEnumerator SimulateCavePattern()  {
        for (int i = 0; i < simulationSteps; i++)   {
            yield return new WaitForSeconds(0);

            //Calculate the new values
            caveGenerator.SmoothMap();

            //Generate texture and display it on the plane
            GenerateAndDisplayTexture(caveGenerator.GetMap());
        }
        Debug.Log("Simulation completed!");
    }



    //Generate a black or white texture depending on if the pixel is cave or wall
    //Display the texture on a plane
    private void GenerateAndDisplayTexture(int[,] data) {
        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                var position = new Vector3Int(x, y, 0);
                var _data = data[x,y];
                var tile = (_data == 0) ? floorTile: (_data == 1 ? wallTile: bushTile);
                tilemap.SetTile(position, tile);
            }
        }
    }

    public int getGridSize() {
        return gridSize;
    }

    public void setGridSize(int newGrid) {
        this.gridSize = newGrid;
    }
}
