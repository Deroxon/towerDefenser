using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover>
{

    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        // activate it to not be null
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        // when spriteRender is enabled
        if(spriteRenderer.enabled)
        {

            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // z position to 0
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

    }

    public void Activate(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        this.spriteRenderer.sprite = sprite;
    }

    public void Deactivate()
    {
        spriteRenderer.enabled = false;
        GameManager.Instance.ClickedBtn = null;
    }
}
