using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using interfaces;
public class Piano : MonoBehaviour, IPossessable
{
	[SerializeField] MovementController movementController;
  
  public UnityEvent<GameObject> soundEvent;
	private AudioSource audioSource;
	
    // Start is called before the first frame update
    void Start()
    {
       if (soundEvent == null)
            soundEvent = new UnityEvent<GameObject>();
		  this.audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void onPossession(){
		movementController.Freeze();
	}

	public void onUnpossession(){
		movementController.Unfreeze();
	}

    public void movementAnimation(float x, float y)
    {
        // Debug.Log("Water can movement animation");
    }
    public void interact()
    {
        this.soundEvent.Invoke(this.gameObject);
		    this.audioSource.Play();
    }
}
