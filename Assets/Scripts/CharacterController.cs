using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
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
    }

    void FixedUpdate()
    {
        this.RB.MovePosition(this.RB.position +  this.movement * this.movementSpeed * Time.fixedDeltaTime);
    }
}
