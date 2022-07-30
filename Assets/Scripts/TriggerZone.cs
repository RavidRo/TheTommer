using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerZone : MonoBehaviour
{
    [SerializeField] private UnityEvent<GameObject> events;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            if(collision.gameObject.tag == "Player")
            {
                triggered = true;
                this.events.Invoke(this.gameObject);
            }
        }
    }
}
