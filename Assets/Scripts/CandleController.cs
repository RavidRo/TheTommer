using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite on;
    public Sprite off;
    private bool turnedOn; 
    // Start is called before the first frame update
    void Start()
    {
       this.turnedOn = true;
       this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void action(){
        if(this.turnedOn)
            this.spriteRenderer.sprite = this.on;
        else
            this.spriteRenderer.sprite = this.off;
        this.turnedOn = !this.turnedOn;
    }
}
