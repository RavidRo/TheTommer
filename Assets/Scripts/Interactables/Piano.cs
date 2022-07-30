using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using interfaces;

[RequireComponent(typeof(AudioSource))]
public class Piano : IPossessable
{
    public UnityEvent<GameObject> soundEvent;
	private AudioSource audioSource;
	
    // Start is called before the first frame update
    void Start()
    {
       if (soundEvent == null)
            soundEvent = new UnityEvent<GameObject>();
		this.audioSource = this.GetComponent<AudioSource>();
		this.canCollide = false;
		this.canMove = false;
	}

	public override void Interact()
    {
        this.soundEvent.Invoke(this.gameObject);
		this.audioSource.Play();
    }
}
