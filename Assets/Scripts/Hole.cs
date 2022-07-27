using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hole : MonoBehaviour
{
    [SerializeField] private GameObject indication;
    public Vector3 direction = Vector3.up;

    private Collider2D collider2d;
    private ContactFilter2D filter = new();

    // Start is called before the first frame update
    void Start()
    {
        collider2d = GetComponent<Collider2D>();

        filter.SetLayerMask(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Holes")));
        filter.useTriggers = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.indication)
        {
            Collider2D[] overlapedColliders = new Collider2D[10];

            int numOfResults = this.collider2d.OverlapCollider(filter, overlapedColliders);
            this.indication.SetActive(numOfResults > 0);
        }
    }
}
