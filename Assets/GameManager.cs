using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //FOR NOW -- THIS WILL CHANGE WHEN WE HAVE MULTIPLE LEVELS
        WallTilemap = GameObject.FindObjectOfType<WallTileMap>().GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //TODO: Make the seting of this private/protected/something
    static public Tilemap WallTilemap;
}
