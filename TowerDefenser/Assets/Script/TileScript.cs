using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    
    public Point GridPosition { get; private set; }

    // property which checking if there is something placed on this tile
    public bool IsEmpty { get; private set; }

    private Color32 fullColor = new Color32(255, 118, 118, 255);

    private Color32 emptyColor = new Color32(96,255,90,255);

    private SpriteRenderer spriteRenderer;

    public Vector2 WorldPosition
    {
        get
        {           /// getting the center position
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        IsEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;
        // every new tiles is added to our TIles dictionary 

        transform.SetParent(parent);

        // instances is added by Singleton to get easier access
        LevelManager.Instance.Tiles.Add(gridPos, this);
    }

    private void OnMouseOver()
    {

        // only execute if mouse isnt on gameobject, like buttton &&if there is no clickedButton
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            if(IsEmpty)
            {
                ColorTile(emptyColor);
            }
            if(!IsEmpty)
            {
                ColorTile(fullColor);
            }

            else if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }

     
    }
    private void OnMouseExit()
    {
        ColorTile(Color.white);
    }

    private void PlaceTower()
    {

        GameObject tower = Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);

        // making it that the towers from botttom will hover the towers from top
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        // setting the tower parent (tile)
        tower.transform.SetParent(transform);

        IsEmpty = false;

        ColorTile(Color.white);

        GameManager.Instance.BuyTower();

    }

    private void ColorTile(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

}
