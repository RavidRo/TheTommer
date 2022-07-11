using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{

    public Transform trackedObject;
    public float offset = 5f; // Offset towards the tracked object movement direction
    public float offsetSmoothing = 4f;

    private Vector2 trackedObjectOldPosition;


    void Update()
    {
        Vector2 trackedObjectNewPosition = (Vector2)this.trackedObject.position;

        Vector2 trackedObjectMovement = trackedObjectNewPosition - this.trackedObjectOldPosition;

        Vector3 trackedObjectMovementDirection3D = Vector3.Normalize(new Vector3(trackedObjectMovement.x, trackedObjectMovement.y, 0));
        Vector2 trackedObjectMovementDirection = (Vector2)trackedObjectMovementDirection3D;

        print(trackedObjectMovement);

        Vector2 newPosition = Vector2.Lerp(
            this.transform.position, 
            (Vector2)this.trackedObject.position + this.offset * trackedObjectMovementDirection, 
            this.offsetSmoothing * Time.deltaTime
        );

        this.transform.position = new Vector3(
            newPosition.x, 
            newPosition.y, 
            this.transform.position.z
        );

        this.trackedObjectOldPosition = trackedObjectNewPosition;
    }
}
