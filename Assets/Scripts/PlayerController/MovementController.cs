using interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementController : MonoBehaviour
{   
    public float movementSpeed = 2f;
    private Rigidbody2D rigidBody;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (DeathController.inAnimation) {
            movement.x = 0;
            movement.y = 0;
            return;
        }
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        this.rigidBody.MovePosition(this.rigidBody.position + this.movement * this.movementSpeed * Time.fixedDeltaTime);
    }

    public Vector2 getMovement(){
        return this.movement;
    }
}
