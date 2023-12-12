using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    // temporary prefab for testing
    public TowerButton ClickedBtn { get;  set; }

    private int currency;

    [SerializeField]
    private Text currenctTxt;

    public int Currency
    {
        get 
        { 
            return currency;
        }

        set 
        {
            this.currency = value;
            this.currenctTxt.text = value.ToString() + " <color=lime>$</color>";
                
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        // 100 = value ^^^
        Currency = 5;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }


    public void PickTower(TowerButton towerBtn)
    {
        if(Currency >= towerBtn.Price)
        {
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);
        }

        
    }

    public void BuyTower()
    {
        if(Currency >= ClickedBtn.Price)
        {
            Currency -= ClickedBtn.Price;
            // deactivate hover 
            Hover.Instance.Deactivate();
        }
       
    }
    private void HandleEscape()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }
}
