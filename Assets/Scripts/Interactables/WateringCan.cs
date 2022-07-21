using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;
public class WateringCan : MonoBehaviour, IPossessable
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void movementAnimation(float x, float y)
    {
        // Debug.Log("Water can movement animation");
    }
    public void interact()
    {
        Debug.Log("water can interact");
    }
}
