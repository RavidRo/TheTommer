using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using interfaces;

[RequireComponent(typeof(SpriteRenderer))]
public class Window : IPossessable
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteClosed;
    [SerializeField] private Sprite spriteOpen;
    public UnityEvent<bool> windowEvent;

    private bool open = false;

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
        this.windowEvent.Invoke(this.open);
    }

    private void UpdateSprite()
    {
        this.spriteRenderer.sprite = this.open ? this.spriteOpen : this.spriteClosed;
    }

}
