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
    }

    public override void OnPossession(){
        this.GetComponent<LightExposure>().enabled = true;
    }
    public override void OnUnpossession(){
		this.GetComponent<LightExposure>().enabled = false;
	}
    public override void Interact()
    {
        Debug.Log("water can interact");
    }
}
