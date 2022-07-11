using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementController : MonoBehaviour
{   
    public float movementSpeed = 5f;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private Vector2 movement;
    

    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        this.animator.SetFloat("xSpeed", this.movement.x);
        this.animator.SetFloat("ySpeed", this.movement.y);

        // print to console
        print(this.movement);
        
        if (Input.GetKeyDown(KeyCode.E)){
            interact();
        }
    }

    void FixedUpdate()
    {
        this.rigidBody.MovePosition(this.rigidBody.position +  this.movement * this.movementSpeed * Time.fixedDeltaTime);
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
