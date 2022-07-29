using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using interfaces;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Window : IPossessable
{
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    [SerializeField] private Sprite spriteClosed;
    [SerializeField] private Sprite spriteOpen;
    [SerializeField] private AudioClip windowOpenClip;
    [SerializeField] private AudioClip windowCloseClip;

    [SerializeField] private float width = 2.5f;
    [SerializeField] private float length = 5f;
    [SerializeField] private Collider2D windBoxCollider; 

    private bool open = false;

    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.audioSource = this.GetComponent<AudioSource>();

        UpdateSprite();

        this.canCollide = false;
        this.canMove = false;
    }

    public override void Interact()
    {
        this.open = !this.open;
        UpdateSprite();

        AudioClip clip = this.open ? windowOpenClip : windowCloseClip;
        if (!clip)
        {
            Debug.LogWarning("Missing audio clip from window interactable");
            return;
        }

        this.audioSource.clip = clip;
        this.audioSource.Play();

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
