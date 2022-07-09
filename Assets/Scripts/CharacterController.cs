using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{   
    public float movementSpeed = 5f;

    private Sprite defaultPlayerSprite;
    private Rigidbody2D RB;
    private SpriteRenderer SR;
    private Vector2 movement;
    
    private GameObject deletedGameObject;


    // Start is called before the first frame update
    void Start()
    {
        this.SR = GetComponent<SpriteRenderer>();
        this.RB = this.GetComponent<Rigidbody2D>();
        
        this.defaultPlayerSprite = this.SR.sprite;
    }
    
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)){
            shapeshift();
        }
    }

    void FixedUpdate()
    {
        this.RB.MovePosition(this.RB.position +  this.movement * this.movementSpeed * Time.fixedDeltaTime);
    }

    void shapeshift() {
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
