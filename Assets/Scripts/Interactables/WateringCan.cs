using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;

public class WateringCan : IPossessable
{
    [SerializeField] private MovementController player;
    [SerializeField] private float wallWidth = 1.5f;
    [SerializeField] private float wallingSpeed = 1f;

    private bool walling = false;
    private float distanceCounter = 0;
    private Hole currentHole;

    private new Collider2D collider;

    public void Start()
    {
        this.canCollide = true;
        this.canMove = true;
        this.canDie = true;
        this.collider = this.GetComponent<Collider2D>();
    }

    public override void Interact()
    {
        if (!this.player)
        {
            Debug.LogError("Assign the player object to the watering can to interact with it");
            return;
        }
        if (!this.walling)
        {
            Collider2D[] overlapedColliders = new Collider2D[10];
            ContactFilter2D filter = new();
            filter.SetLayerMask(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Holes")));
            filter.useTriggers = true;
            int numOfResults = this.collider.OverlapCollider(filter, overlapedColliders);
            if (numOfResults > 0)
            {
                this.currentHole = overlapedColliders[0].GetComponent<Hole>();
                this.collider.isTrigger = true;
                this.player.Freeze();
                this.distanceCounter = 0;
                this.walling = true;
            }
        }
    }

    public override void Update()
    {
        base.Update();

        if (walling)
        {
            float addedDistance = this.wallingSpeed * Time.deltaTime;
            this.distanceCounter += addedDistance;
            this.player.transform.position += this.currentHole.direction * addedDistance;

            if(this.distanceCounter >= this.wallWidth)
            {
                this.collider.isTrigger = false;
                this.walling = false;
                this.player.Unfreeze();
                this.currentHole = null;
            }
        }
    }
}
