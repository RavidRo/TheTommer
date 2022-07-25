using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;
public class Piano : IPossessable
{
	[SerializeField] MovementController movementController;
	private AudioSource audioSource;
	
    // Start is called before the first frame update
    void Start()
    {
		this.audioSource = this.GetComponent<AudioSource>();

		this.canCollide = false;
		this.canMove = false;
	}

	public override void Interact()
    {
		this.audioSource.Play();
    }
}
