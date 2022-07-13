using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float moveSpeed = 0.2f;
    [SerializeField] float closeEnough = 0.5f;
    [SerializeField] float meanWaitingTime = 3f;
    [SerializeField] float waitingTimeRandomVariation = 1f;
    [SerializeField] bool waiting = false;

    public Animator animator;
    private int destPoint = 0;
    private float decisionTimeCount = 0;
    private float currentWaitingTime;

    void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (this.patrolPoints.Length == 0)
            return;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        this.destPoint = (this.destPoint + 1) % this.patrolPoints.Length;
    }

    void Update()
    {
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

    void Wait()
    {
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", 0);

        this.decisionTimeCount += Time.deltaTime;
    }

    void Move(Vector2 currentPosition, Vector2 nextPoint)
    {
        Vector2 direction = (nextPoint - currentPosition).normalized;

        float xDir = direction.x;
        float yDir = direction.y;

        Vector2 newPosition = direction * Time.deltaTime * moveSpeed;
        this.gameObject.transform.position += new Vector3(newPosition.x, newPosition.y, 0);

        if (animator)
        {
            animator.SetFloat("MoveX", xDir);
            animator.SetFloat("MoveY", yDir);
        }
    }
}
