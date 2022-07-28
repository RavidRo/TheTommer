using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    private void UpdateSprite()
    {
        this.spriteRenderer.sprite = this.open ? this.spriteOpen : this.spriteClosed;
    }

}
