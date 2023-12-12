using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager> 
{

    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    [SerializeField]
    private Transform map;

    public Dictionary<Point, TileScript> Tiles { get; set; }

    private Point GreenSpawn, PurpleSpawn;

    [SerializeField]
    private GameObject GreenPortal, PurplePortal;

    public float TileSize {
        get
        { // we taking the prefabs from the gameobject LevelManger for example "grass", "stone way" etc
           return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        }
    }

    // Start is called before the first frame update
    void Start()
    {


        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void CreateLevel()
    {

        Tiles = new Dictionary<Point, TileScript>();

        string[] mapData = ReadLevelText();
        
        // taking first element and making it into char array - example = [0,0,0,0] and taking length of it
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height));


        for (int y = 0; y < mapY; y++) // the y position
        {
            // like the line with mapX but we taking the content of the array into char array
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapX; x++) {

                // Places the tiles in the world
              PlaceTile(newTiles[x].ToString(),  x,y, worldStart);
            }


        }
        // last position of tiles
        maxTile= Tiles[new Point(mapX-1, mapY-1)].transform.position;

        
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y -TileSize) ); // executing camera movement and feeding it by last value of maxTile

        SpawnPortals();

    }


    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        // "1" == 1
        int tileIndex = int.Parse(tileType);


        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        // we passing an refernce to Setup and creating a new point with transforming an position
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map );

        // making it that the towers will cover the tiles
        newTile.GetComponent<SpriteRenderer>().sortingOrder = -1;
    }

    public string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        // by Replace we just making it to looks like 10001000-1000000 instead like int txt document
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);



        return data.Split("-");
    }

    private void SpawnPortals()
    {
        GreenSpawn = new Point(0, 0);
        PurpleSpawn = new Point(21, 9);
        

        // spawning portal on first Tile using Tiles and tranform it into position, quaternion means  no rotation
        Instantiate(GreenPortal, Tiles[GreenSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);

        Instantiate(PurplePortal, Tiles[PurpleSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }

}
