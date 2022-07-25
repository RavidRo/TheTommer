using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{

    private GameObject possessing;

    // Update is called once per fram
    public void action()
    {
        Debug.Log("possess action");
        if (possessing == null)
        {
            action();
        }
    }
    void possess()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.2f, 1 << LayerMask.NameToLayer("ShiftingObjects"));
        // If standing on an object, change to the other object
        if (collider)
        {
            GameObject possessable = collider.gameObject;
            this.possessing = possessable;
            // this.gameObject.SetActive(false);
            this.gameObject.transform.position = this.possessing.transform.position;
            this.possessing.transform.SetParent(this.gameObject.transform);
            this.possessing.GetComponent<Collider2D>().isTrigger = false;
        }
    }

}
