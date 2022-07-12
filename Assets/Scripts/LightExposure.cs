using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;

public class LightExposure : MonoBehaviour
{

    [SerializeField] MovementController movementController;
    [SerializeField] float deathRadius = 1.2f;
    [SerializeField] float warningRadius = 1.75f;
    [SerializeField] GameObject warningIcon;
    [SerializeField] bool invincible = false;

    [SerializeField]  Color warningColorNear = Color.red;
    [SerializeField]  Color warningColorFar = Color.yellow;

    private Animator animator;
    private List<GameObject> lightSources = new();
    private SpriteRenderer warningIconSprite;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        if(this.warningIcon != null)
        {
            this.warningIconSprite = this.warningIcon.GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        float minDistance = this.CalculateMinDistance();

        if (minDistance <= this.deathRadius)
        {
            this.Die();
            return;
        }

        bool inWarningRadius = minDistance < this.warningRadius;
        if (inWarningRadius)
        {
            SetWarningColor(minDistance);
        }
        else
        {
            this.warningIcon.SetActive(false);
        }
    }

    float MapValue(float mainValue, float inValueMin, float inValueMax, float outValueMin, float outValueMax)
    {
        // Min-Max Normalization
        return (mainValue - inValueMin) * (outValueMax - outValueMin) / (inValueMax - inValueMin) + outValueMin;
    }

    void SetWarningColor(float distance)
    {
        if (!dead)
        {
            this.warningIcon.SetActive(true);

            //Convert 0 and 200 distance range to 0f and 1f range
            float lerp = this.MapValue(distance, this.deathRadius, this.warningRadius, 0f, 1f);
            //Lerp Color between near and far color
            Color lerpColor = Color.Lerp(this.warningColorNear, this.warningColorFar, lerp);

            // this.warningIconSprite.material.color = lerpColor;
            this.warningIconSprite.color = lerpColor;
        }
    }

    void Die()
    {
        if (!invincible)
        {
            this.dead = true;
            if (this.animator != null)
            {
                this.animator.SetTrigger("dead");
            }
            if (this.movementController != null)
            {
                this.movementController.OnDeath();
            }
            if(this.warningIcon != null)
            {
                this.warningIcon.SetActive(false);
            }
        }
    }

    // Return the distance to the closest light source
    float CalculateMinDistance()
    {
        float min = int.MaxValue;
        foreach (GameObject source in this.lightSources)
        {
            // If there is not something in between
            if (!Physics2D.Linecast(this.transform.position, source.transform.position))
            {
                float distance = Vector2.Distance(this.transform.position, source.transform.position);
                min = Mathf.Min(min, distance);
            }
        }
        return min;
    }

    void OnTriggerEnter2D(Collider2D c) //change to 2d for 2d
    {
        if (!c.gameObject.CompareTag("LightExposure")) return;

        this.lightSources.Add(c.gameObject);
    }

    void OnTriggerExit(Collider c) //change to 2d for 2d
    {
        this.lightSources.Remove(c.gameObject);
    }
}
