using System;
using Random=System.Random;
using UnityEngine;

/**
 * This class is used to generate a random "cave" map.
 * The map is generated as a two-dimensional array of ints, where "0" denotes floor and "1" denotes wall.
 * Initially, the boundaries of the cave are set to "wall", and the inner cells are set at random.
 * Then, a cellular automaton is run in order to smooth out the cave.
 * 
 * Based on Unity tutorial https://www.youtube.com/watch?v=v7yyZZjF1z4 
 * Code by Habrador: https://github.com/Habrador/Unity-Programming-Patterns/blob/master/Assets/Patterns/7.%20Double%20Buffer/Cave/GameController.cs
 * Using a double-buffer technique explained here: https://github.com/Habrador/Unity-Programming-Patterns#7-double-buffer
 * 
 * Adapted by: Erel Segal-Halevi
 * Since: 2020-12
 */
public class NewCaveGenerator {
    protected float[] randomFillPercent;
    protected int gridSize;

    //The index of the wall type
    protected int wallIndex;

    //The double buffer
    private int[,] bufferOld;
    private int[,] bufferNew;

    private Random random;

    public NewCaveGenerator(float[] randomFillPercent=null, int gridSize=100, int wallIndex=0) {
        this.randomFillPercent = randomFillPercent;
        this.gridSize = gridSize;

        this.bufferOld = new int[gridSize, gridSize];
        this.bufferNew = new int[gridSize, gridSize];

        this.wallIndex = wallIndex;

        random = new Random();
    }

    public int[,] GetMap() {
        return bufferOld;
    }

    /**
     * Generate a random map.
     * The map is not smoothed; call Smooth several times in order to smooth it.
     */
    public void RandomizeMap()  {
        //Init the old values so we can calculate the new values
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                if (x == 0 || x == gridSize - 1 || y == 0 || y == gridSize - 1) {
                    //We dont want holes in our walls, so the border is always a wall
                    bufferOld[x, y] = wallIndex;
                } else {
                    //Random walls and caves
                    double n = new Random().NextDouble();
                    Debug.Log(n);
                    double v =0;
                    for (int i=0; i < randomFillPercent.Length; i++){
                        if (v <= n && (n < v + randomFillPercent[i])) {
                            bufferOld[x,y] = i;
                            break;
                        }
                        v += randomFillPercent[i]; 
                    }
                }
            }
        }
    }

    /**
     * Generate caves by smoothing the data
     * Remember to always put the new results in bufferNew and use bufferOld to do the calculations
     */
    public void SmoothMap()   {
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                //Border is always wall
                if (x == 0 || x == gridSize - 1 || y == 0 || y == gridSize - 1) {
                    bufferNew[x, y] = wallIndex;
                    continue;
                }

                //Use some smoothing rules to generate caves
                //check witch type is the most neighbors
                int maxCountNeighbersType = 0;
                int chosenType =0;
                for (int i = 0; i < randomFillPercent.Length; i++){
                    if (GetSurroundingTypeCount(x, y, i) >= maxCountNeighbersType){
                        maxCountNeighbersType = GetSurroundingTypeCount(x, y,i);
                        chosenType = i;
                    }
                }
                bufferNew[x, y] = chosenType;

            }
        }

        //Swap the pointers to the buffers
        (bufferOld, bufferNew) = (bufferNew, bufferOld);
    }



    //Given a cell, how many of the 8 surrounding cells are walls?
    private int GetSurroundingTypeCount(int cellX, int cellY, int type) {
        int Counter = 0;
        for (int neighborX = cellX - 1; neighborX <= cellX + 1; neighborX ++) {
            for (int neighborY = cellY - 1; neighborY <= cellY + 1; neighborY++) {
                //This is the cell itself and no neighbor!
                if (neighborX == cellX && neighborY == cellY) {
                    continue;
                }
                //This neighbor is a the same type
                if (bufferOld[neighborX, neighborY] == type) {
                    Counter++;
                }
            }
        }
        return Counter;
    }
}
