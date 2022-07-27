using interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeathController : MonoBehaviour, IDeathSubscriber
{
    private Animator animator;
    [SerializeField] GameObject tommer;
    [SerializeField] PlayerController player;
    [SerializeField] float deathAnimationTime = 1.5f;
    [SerializeField] float spawnAnimationTime = 1f;
    [SerializeField] AudioSource audioSource;
    private float animationTimerCount = 0;
    private Vector3 initialSpawn;
    private bool spawning = false;
    public static bool inAnimation = false;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.tommer.GetComponent<Animator>();
        this.initialSpawn = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        inAnimation = false;
        if (this.dead)
        {
            inAnimation = true;
            this.animationTimerCount += Time.deltaTime;
            if (this.animationTimerCount >= this.deathAnimationTime)
            {
                this.animationTimerCount = 0;
                this.dead = false;
                this.spawning = true;
                // this.transform.position = this.initialSpawn;
                this.transform.position = this.initialSpawn;
                this.animator.SetTrigger("spawn");
            }
            return;
        }
        if (this.spawning)
        {
            inAnimation = true;
            this.animationTimerCount += Time.deltaTime;
            if (this.animationTimerCount >= this.spawnAnimationTime)
            {
                this.animationTimerCount = 0;
                this.spawning = false;
            }
            return;
        }
    }


    public void OnDeath()
    {
        this.player.unpossess();
        this.dead = true;
        audioSource.Play();
        this.animator.SetTrigger("dead");
    }
}
