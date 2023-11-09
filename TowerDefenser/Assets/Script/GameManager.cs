using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    // temporary prefab for testing
    public TowerButton ClickedBtn { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PickTower(TowerButton towerBtn)
    {
        this.ClickedBtn = towerBtn;
        Hover.Instance.Activate(towerBtn.Sprite);
    }

    public void BuyTower()
    {
        // reseting clicked button to null ~ avoid to place few times in a row 
        ClickedBtn = null; 
    }
}
