using interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour, IDeathSubscriber
{
    private Animator animator;

    [SerializeField] GameObject tommer;
    [SerializeField] PlayerController player;
    [SerializeField] float deathAnimationTime = 1.5f;
    [SerializeField] float finalDeadAnimationTime = 7f;
    [SerializeField] float spawnAnimationTime = 1f;
    [SerializeField] AudioSource audioSourceDeath;
    [SerializeField] AudioSource audioSourceFinalDeath;
    [SerializeField] Animator UIDeathAnimator;

    private float animationTimerCount = 0;
    private Vector3 initialSpawn;
    private bool spawning = false;
    private bool dead = false;
    private bool finalDead = false;

    private static readonly int INITIAL_LIVES = 1;
    public static int lives = INITIAL_LIVES;
    public static bool inAnimation = false;

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
        if(this.finalDead){
            inAnimation = true;
            this.animationTimerCount += Time.deltaTime;
            if(this.animationTimerCount >= this.finalDeadAnimationTime){
                lives = INITIAL_LIVES;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
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
        this.player.Unpossess();
        this.audioSourceDeath.Play();
        this.animator.SetTrigger("dead");
        if((--lives) <= 0){
            this.audioSourceFinalDeath.Play();
            this.finalDead = true;
            if(this.UIDeathAnimator != null){
                UIDeathAnimator.SetTrigger("died");
            }

        }
        else{
            this.dead = true;
        }
    }
}
