using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{

    private Sprite defaultPlayerSprite;
    private SpriteRenderer SR;
    private GameObject deletedGameObject;


    // Start is called before the first frame update
    void Start()
    {
        this.SR = GetComponent<SpriteRenderer>();        
        this.defaultPlayerSprite = this.SR.sprite;
    }
    
    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space)){
            posses();
        }
    }

    void posses() {
        // Check if standing on an object
        Collider2D collider =  Physics2D.OverlapCircle(transform.position, 0.2f, 1 << LayerMask.NameToLayer("ShiftingObjects"));

        // If standing on an object, change to the other object
        if (collider){
            GameObject shapeshiftTo = collider.gameObject;
            this.SR.sprite = shapeshiftTo.GetComponent<SpriteRenderer>().sprite;
            this.deletedGameObject = shapeshiftTo;
            // Hide the old object
            shapeshiftTo.SetActive(false);
        }
        // Return to default sprite
        else{
            this.SR.sprite = this.defaultPlayerSprite;
            if (this.deletedGameObject != null){
                // Move the old object to the current position and show  it
                this.deletedGameObject.transform.position = this.transform.position;
                this.deletedGameObject.SetActive(true);
                this.deletedGameObject = null;
            }
        }
    }
}
