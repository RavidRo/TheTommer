using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using interfaces;
public class CandleController : MonoBehaviour, ILightable
{
    public SpriteRenderer spriteRenderer;
    public Sprite lit;
    public Sprite unlit;
    private bool isLit; 
    [SerializeField] private GameObject candleLight;
    [SerializeField] float relightCandleTime = 1.5f; 

    // Start is called before the first frame update
    void Start()
    {
        this.isLit = true;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void onWind(){
        if(isLit){
            this.spriteRenderer.sprite = unlit;
            this.candleLight.SetActive(false);
        }
    }

    public IEnumerator onTurnOn(){
        if(!isLit){
            yield return new WaitForSeconds(relightCandleTime);
            this.spriteRenderer.sprite = lit;
            this.candleLight.SetActive(true);
        }
    } 
}
