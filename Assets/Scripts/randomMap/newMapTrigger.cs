using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class newMapTrigger : MonoBehaviour
{
    // [SerializeField] string triggingTag;
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject player;
    [SerializeField] GameObject plane;
    NewTilemapCaveGenerator tilemapCaveGenerator;
    int gridSize;

    void Start (){
        tilemapCaveGenerator = tilemap.GetComponent<NewTilemapCaveGenerator>();
        gridSize = tilemapCaveGenerator.getGridSize();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(player.tag == collision.tag){
            Debug.Log("trigger");
            gridSize += 10;
            tilemapCaveGenerator.setGridSize(gridSize);
            tilemapCaveGenerator.initMap();
            Debug.Log(tilemap.WorldToCell(new Vector3Int(1,1,0)));
            player.transform.position = tilemap.layoutGrid.CellToWorld(new Vector3Int(8,8,0));
            gameObject.transform.position = tilemap.layoutGrid.CellToWorld(new Vector3Int(gridSize-8,gridSize-8,0));
            plane.transform.localScale +=new Vector3(1.0f,0.0f,1.0f);
        }
    }
}
