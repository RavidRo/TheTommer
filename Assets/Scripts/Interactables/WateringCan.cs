using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;
public class WateringCan : IPossessable
{

    public void Start()
    {
        this.canCollide = true;
        this.canMove = true;
        this.canDie = true;
    }

    public override void Interact()
    {
        Debug.Log("water can interact");
    }
}
