using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScene : MonoBehaviour
{
    [SerializeField] MovementController player;
    [SerializeField] Transform window;
    [SerializeField] FollowingCamera mainCamera;

    [SerializeField] float windowWaitTime = 2f;
    private float windowWaitTimeCount = 0;
    private bool waitTimeStarted = false;

    [SerializeField] DialogController speachBubble;

    private bool triggered = false;

    private void Update()
    {
        if (waitTimeStarted)
        {
            windowWaitTimeCount += Time.deltaTime;
            if (windowWaitTimeCount >= windowWaitTime)
            {
                waitTimeStarted = false;
                windowWaitTimeCount = 0;
                returnToPlayer();
            }
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            triggered = true;

            // Freeze player
            player.Freeze();

            // Move camera to window
            mainCamera.trackedObject = window;

            // Waif a few moments
            waitTimeStarted = true;
        }
    }

    private void returnToPlayer()
    {
        // Return to player
        mainCamera.trackedObject = player.transform;

        // Trigger speach bubble
        speachBubble.StartNextDialog();

        // Unfreeze player
        player.Unfreeze();
    }
}
