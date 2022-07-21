using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;
public class PlayerController : MonoBehaviour
{
    public GameObject Tommer; 
    private GameObject possessing; 
    

    // public  Sprite
    // Start is called before the first frame update
    void Start()
    {
        this.Tommer.transform.position=this.gameObject.transform.position;
        this.Tommer.transform.SetParent(this.transform);
    }

    void FixedUpdate(){
        Vector2 movement = this.GetComponent<MovementController>().getMovement();
        if(this.possessing != null ) this.possessing.GetComponent<Ipossessable>().movementAnimation(movement.x, movement.y);
        else this.Tommer.GetComponent<TommerController>().movementAnimation(movement.x, movement.y);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            Debug.Log("E");
            interact();
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("space");
            if(this.possessing == null) possess();
            else unpossess();
        }
    }

    void interact(){
        if(this.possessing != null ) this.possessing.GetComponent<Ipossessable>().interact();
        else this.Tommer.GetComponent<TommerController>().interact();
    } 

    void unpossess(){
        this.possessing.transform.SetParent(null);
        this.possessing.GetComponent<Collider2D>().isTrigger=true;
        this.possessing = null;
        this.Tommer.SetActive(true);
    }

    void possess(){
        Collider2D collider =  Physics2D.OverlapCircle(transform.position, 0.2f, 1 << LayerMask.NameToLayer("ShiftingObjects"));
         // If standing on an object, change to the other object
        if (collider){
            GameObject possessable = collider.gameObject;
            this.possessing = possessable;
            this.Tommer.SetActive(false);
            this.possessing.transform.position=this.gameObject.transform.position;
            this.possessing.transform.SetParent(this.gameObject.transform);
            this.possessing.GetComponent<Collider2D>().isTrigger=false;
        }
    }

}
