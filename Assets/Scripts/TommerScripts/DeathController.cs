using interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeathController : MonoBehaviour, IDeathSubscriber
{   
    private Animator animator;
    [SerializeField] float deathAnimationTime = 3f;
    [SerializeField] float spawnAnimationTime = 3f;
    private bool dead = false;
    private float animationTimerCount = 0;
    private Vector3 initialSpawn;
    private bool spawning = false;
    public static bool inAnimation=false;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.initialSpawn = this.transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (this.dead)
        {
            this.animationTimerCount += Time.deltaTime;
            if(this.animationTimerCount >= this.deathAnimationTime)
            {
                this.animationTimerCount = 0;
                this.dead = false;
                this.spawning = true;
                // this.transform.position = this.initialSpawn;
                this.transform.parent.transform.position = this.initialSpawn;
                this.animator.SetTrigger("spawn");
            }
            return;
        }
        if(this.spawning)
        {
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
        this.dead = true;
        this.animator.SetTrigger("dead");
    }
}
