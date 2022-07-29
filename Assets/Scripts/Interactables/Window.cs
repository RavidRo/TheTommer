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

    [SerializeField] private float width = 2.5f;
    [SerializeField] private float length = 5f;
    [SerializeField] private Collider2D windBoxCollider; 
    // public UnityEvent<bool> windowEvent;

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
        // this.windowEvent.Invoke(this.open);
        if(this.open)
            turnOffLights();
             
    }
    public void turnOffLights(){
        GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag("LightExposure");
        foreach (var g in gameObjects)
        {   
            if(this.windBoxCollider.OverlapPoint(g.transform.position)){
                g.transform.parent.gameObject.GetComponent<ILightable>().onWind();
            }
        }
    }
    private void UpdateSprite()
    {
        this.spriteRenderer.sprite = this.open ? this.spriteOpen : this.spriteClosed;
    }

}
