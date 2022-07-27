using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;

[RequireComponent(typeof(SpriteRenderer))]
public class Window : IPossessable
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteClosed;
    [SerializeField] private Sprite spriteOpen;

    private bool open = false;

    [SerializeField] MovementController movementController;

    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        UpdateSprite();

        this.canCollide = false;
        this.canMove = false;
    }

    public override void Interact()
    {
        this.open = !this.open;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        this.spriteRenderer.sprite = this.open ? this.spriteOpen : this.spriteClosed;
    }

}
