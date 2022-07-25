using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;
public class TommerController : IPossessable
{
    // public  Sprite
    // Start is called before the first frame update

    private Animator animator;
    private Rigidbody2D rigidBody;

    void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
    }

    public override void MovementAnimation(float x, float y)
    {
        this.animator.SetFloat("xSpeed", x);
        this.animator.SetFloat("ySpeed", y);
    }
}
