using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;

[RequireComponent(typeof(MovementController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private TommerController Tommer;
    private IPossessable possessing;
    private MovementController movementController;

    void Start()
    {
        this.movementController = this.GetComponent<MovementController>();
        this.Tommer.transform.position = this.gameObject.transform.position;
        this.Tommer.transform.SetParent(this.transform);
    }

    void FixedUpdate()
    {
        Vector2 movement = this.movementController.getMovement();
        if (this.possessing != null) this.possessing.MovementAnimation(movement.x, movement.y);
        else this.Tommer.MovementAnimation(movement.x, movement.y);
    }

    void Update()
    {
        if(DeathController.inAnimation) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            interact();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (this.possessing == null) possess();
            else unpossess();
        }
    }

    void interact()
    {
        if (this.possessing != null) this.possessing.Interact();
        else this.Tommer.Interact();
    }

    public void unpossess()
    {
        if(this.possessing != null){
            this.possessing.transform.SetParent(null);
            this.possessing.GetComponent<Collider2D>().isTrigger = true;
            this.movementController.Unfreeze();
            this.possessing.OnUnpossession();
            this.possessing = null;
            this.Tommer.gameObject.SetActive(true);
        }
    }

    void possess()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.2f, 1 << LayerMask.NameToLayer("ShiftingObjects"));
        // If standing on an object, change to the other object
        if (collider)
        {
            // Save new object
            this.possessing = collider.gameObject.GetComponent<IPossessable>();
            IPossessable possableComponent = possessing.GetComponent<IPossessable>();

            // Replace Tommer
            this.Tommer.gameObject.SetActive(false);
            this.gameObject.transform.position = this.possessing.transform.position;
            this.possessing.transform.SetParent(this.gameObject.transform);

            // Fire events
            if (possableComponent.CanCollide)
            {
                this.possessing.GetComponent<Collider2D>().isTrigger = false;
            }
            if (!possableComponent.CanMove)
            {
                this.movementController.Freeze();
            }
            this.possessing.GetComponent<IPossessable>().OnPossession();
        }
    }

}
