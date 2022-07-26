using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;

[RequireComponent(typeof(Animator))]
public class TommerController : IPossessable
{
    private Animator animator;

    private bool endingScene = false;

    void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    public override void MovementAnimation(float x, float y)
    {
        this.animator.SetFloat("xSpeed", x);
        this.animator.SetFloat("ySpeed", y);
    }

    public void Update()
    {
        if (this.endingScene)
        {
            this.transform.parent.transform.position = this.transform.position;
        }
    }

    public void EndingScene()
    {
        this.endingScene = true;
        this.animator.SetTrigger("endingScene");
    }
}
