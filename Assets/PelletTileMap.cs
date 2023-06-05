using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PelletTileMap : MonoBehaviour
{
    //TODO: Add code for pellet eaters to signal updating this whenever they die/respawn
    PelletEater[] pelletEaters;
    Tilemap myTileMap;

    public int PelletPoints = 1;
    public bool RequiredForLevelCompletion = false;
    public float PowerSeconds = 0;

    void Start()
    {
        pelletEaters = GameObject.FindObjectsOfType<PelletEater>();
        myTileMap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(PelletEater pe in pelletEaters){
            CheckPellet(pe);
        }
    }
    void CheckPellet(PelletEater pelletEater){
        Vector2 offsetPosition = (Vector2)pelletEater.transform.position + new Vector2(0.5f, 0.5f);
        TileBase tile = GetTileAt((Vector2)offsetPosition);
        if(tile==null){
            return;
        }
        Debug.Log("NOM");
        EatPelletAt((Vector2)offsetPosition);
    }
    void EatPelletAt(Vector2 pos){
        SetTileAt(pos, null);

        //TODO: do the thing, points and what not
    }

    TileBase GetTileAt(Vector2 pos){
        Vector3Int cellPos = myTileMap.WorldToCell(pos);
        return myTileMap.GetTile(cellPos);
    }
    void SetTileAt(Vector2 pos, TileBase tile){
        Vector3Int cellPos = myTileMap.WorldToCell(pos);
        myTileMap.SetTile(cellPos, tile);
    }
}
