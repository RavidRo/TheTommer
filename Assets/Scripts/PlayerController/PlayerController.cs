using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;

[RequireComponent(typeof(LightExposure))]
[RequireComponent(typeof(MovementController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public TommerController Tommer;
    public IPossessable possessing;
    private MovementController movementController;
    private LightExposure lightExposure;

    void Start()
    {
        this.movementController = this.GetComponent<MovementController>();
        this.lightExposure = this.GetComponent<LightExposure>();
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
            Interact();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if(this.possessing == null)
            {
                if(Input.GetKeyDown(KeyCode.Space)) {
                    TryPossesing();
                }
            }
            else if (this.possessing.IsPossionComplete())
            {
                this.possessing.CancelPossession();
                Possess();
            }
            else if (this.possessing.IsPossessing())
            {
                GameObject possasable = GetNearPossable();
                if (!possasable || possasable.GetInstanceID() != this.possessing.gameObject.GetInstanceID())
                {
                    this.possessing.CancelPossession();
                    this.possessing = null;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Unpossess();
                }
            }
        }
        else
        {
            if (this.possessing != null && this.possessing.IsPossessing())
            {
                this.possessing.CancelPossession();
                this.possessing = null;
            }
        }
    }

    void Interact()
    {
        if (this.possessing != null) this.possessing.Interact();
        else this.Tommer.Interact();
    }

    public void Unpossess()
    {
        if(this.possessing != null){
            this.possessing.transform.SetParent(null);
            this.possessing.GetComponent<Collider2D>().isTrigger = true;
            this.movementController.Unfreeze();
            this.possessing.OnUnpossessionGeneric();
            this.possessing = null;
            this.lightExposure.enabled = true;
            this.Tommer.gameObject.SetActive(true);
        }
    }

    GameObject GetNearPossable()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.2f, 1 << LayerMask.NameToLayer("ShiftingObjects"));
        // If standing on an object, change to the other object
        if (collider)
        {
            return collider.gameObject;
        }
        return null;
    }

    void TryPossesing()
    {
        GameObject possable = GetNearPossable();
        // If standing on an object, change to the other object
        if (possable)
        {
            // Save new object
            this.possessing = possable.GetComponent<IPossessable>();
            this.possessing.LoadPossesion();
        }
    }

    void Possess()
    {
        // Replace Tommer
        this.Tommer.gameObject.SetActive(false);
        this.gameObject.transform.position = this.possessing.transform.position;
        this.possessing.transform.SetParent(this.gameObject.transform);

        // Fire events
        if (this.possessing.CanCollide)
        {
            this.possessing.GetComponent<Collider2D>().isTrigger = false;
        }
        if (!this.possessing.CanMove)
        {
            this.movementController.Freeze();
        }
        if (!this.possessing.CanDie)
        {
            this.lightExposure.enabled = false;
        }
        this.possessing.GetComponent<IPossessable>().OnPossessionGeneric();
        
    }

}
