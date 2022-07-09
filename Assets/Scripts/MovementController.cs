using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementController : MonoBehaviour
{   
    public float movementSpeed = 5f;

    private Rigidbody2D RB;
    private Vector2 movement;
    

    // Start is called before the first frame update
    void Start()
    {
        this.RB = this.GetComponent<Rigidbody2D>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.E)){
            interact();
        }
    }

    void FixedUpdate()
    {
        this.RB.MovePosition(this.RB.position +  this.movement * this.movementSpeed * Time.fixedDeltaTime);
    }
    void interact()
    {
        Collider2D collider =  Physics2D.OverlapCircle(transform.position, 0.2f, 1 << LayerMask.NameToLayer("ShiftingObjects"));

        // If standing on an object, change to the other object
        if (collider){
            GameObject gameObject = collider.gameObject;
            // gameObject.GetComponent<>();
        }
    }
}
