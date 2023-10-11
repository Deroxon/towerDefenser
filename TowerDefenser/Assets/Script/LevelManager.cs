using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

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

                // Places the tiles int he world
              maxTile =  PlaceTile(newTiles[x].ToString(),  x,y, worldStart);
            }


        }
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y -TileSize) ); // executing camera movement and feeding it by last value of maxTile

    }


    private Vector3 PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        // "1" == 1
        int tileIndex = int.Parse(tileType);


        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);
        newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);

        return newTile.transform.position;
    }

    public string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        // by Replace we just making it to looks like 10001000-1000000 instead like int txt document
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);



        return data.Split("-");
    }

}
