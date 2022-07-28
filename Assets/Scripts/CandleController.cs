using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class CandleController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite lit;
    public Sprite unlit;
    private bool isLit; 
    [SerializeField] private GameObject candleLight;
    // Start is called before the first frame update
    void Start()
    {
        this.turnedOn = true;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void onWindowEvent(bool isOpen){
        
    }

}
