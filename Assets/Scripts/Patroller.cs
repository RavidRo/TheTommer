using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using interfaces; 

public class Patroller : MonoBehaviour, ILightable
{
    [SerializeField] List<Transform> patrolPoints;
    [SerializeField] float moveSpeed = 0.2f;
    [SerializeField] float closeEnough = 1f;
    [SerializeField] float meanWaitingTime = 2f;
    [SerializeField] float waitingTimeRandomVariation = 1f;
    [SerializeField] bool waiting = false;
    [SerializeField] GameObject alertSprite; 
    [SerializeField] GameObject candleLight;
    [SerializeField] float relightCandleTime = 1.5f; 
    [SerializeField] float hearingDistance = 10f;

    public Animator animator;
    private int destPoint = 0;
    private float decisionTimeCount = 0;
    private float currentWaitingTime;

    private Path path; 
    private int currentWaypoint=0;
    //private bool reachedEndOfPath;
    private Rigidbody2D rb;
    private Seeker seeker;
    
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.seeker = this.GetComponent<Seeker>();
        this.rb = this.GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }
    void UpdatePath(){
        if(this.seeker.IsDone())
            this.seeker.StartPath(this.rb.position, this.patrolPoints[this.destPoint].position, OnPathComplete);
    }
    void OnPathComplete(Path p){
        if(!p.error){
            this.path=p;
            this.currentWaypoint = 0;
        }   
    }
    void FixedUpdate()
    {
        // Returns if no points have been set up
        if (this.patrolPoints.Count == 0)
            return;

        Vector2 currentPosition = this.gameObject.transform.position;
        Vector2 nextPoint = this.patrolPoints[this.destPoint].position;

        if (!waiting)
        {
            this.Move(currentPosition, nextPoint);

            // Choose the next destination point when the agent gets
            // close to the current one.
            float remainingDistance = Vector2.Distance(currentPosition, nextPoint);
            if (remainingDistance < this.closeEnough)
            {
                this.waiting = true;
                this.currentWaitingTime = this.meanWaitingTime + Random.Range(-this.waitingTimeRandomVariation, this.waitingTimeRandomVariation);
            }
        }
        else
        {
            Wait();
            if (this.decisionTimeCount >= this.currentWaitingTime)
            {
                this.decisionTimeCount = 0;
                this.waiting = false;
                this.GotoNextPoint();
            }
        }
    }
    void GotoNextPoint()
    {
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.

        // this.destPoint = (this.destPoint + 1) % this.patrolPoints.Count;
        this.destPoint = (int)Random.Range(0, this.patrolPoints.Count);
    }

    void Wait()
    {
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", 0);

        this.decisionTimeCount += Time.deltaTime;
    }

    void Move(Vector2 currentPosition, Vector2 nextPoint)
    {

        if(path == null)
            return;
        //if(this.currentWaypoint >= path.vectorPath.Count){
        //    reachedEndOfPath = true;
        //    return;
        //}
        //else{
        //    reachedEndOfPath = false;
        //}
        Vector2 direction = ((Vector2)path.vectorPath[this.currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * Time.deltaTime * moveSpeed;
        
        this.gameObject.transform.position += new Vector3(force.x, force.y, 0);

        float distance = Vector2.Distance(rb.position, path.vectorPath[this.currentWaypoint]);
        if(distance < this.closeEnough){
            this.currentWaypoint++;
        }
        if (animator)
        {
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }
    }

    public void onSound(GameObject g){
        if(Vector2.Distance(this.transform.position, g.transform.position) < hearingDistance){
            this.patrolPoints.Remove(g.transform);
            this.patrolPoints.Insert(0, g.transform);
            this.waiting=false;
            this.currentWaitingTime=0;
            this.destPoint=0;
            StartCoroutine(this.alertSprite.GetComponent<AlertPopup>().onPopUp());
            }
    }

    public void onWind(){
        this.candleLight.SetActive(false);
        StartCoroutine(onTurnOn());
    }

    public IEnumerator onTurnOn(){
        yield return new WaitForSeconds(this.relightCandleTime);
        this.candleLight.SetActive(true);
    }
}
