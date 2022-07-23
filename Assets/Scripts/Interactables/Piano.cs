using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;
public class Piano : MonoBehaviour, IPossessable
{
	[SerializeField] MovementController movementController;
	private AudioSource audioSource;
	
    // Start is called before the first frame update
    void Start()
    {
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
		this.audioSource.Play();
    }
}
