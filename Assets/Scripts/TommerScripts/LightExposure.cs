using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;

public class LightExposure : MonoBehaviour
{

    [SerializeField] DeathController deathController;
    [SerializeField] GameObject warningIcon;
    [SerializeField] bool invincible = false;

    [SerializeField] Color warningColorNear = Color.red;
    [SerializeField] Color warningColorFar = Color.yellow;

    [SerializeField] float warningDistance = 1f;
    private class LightSource
    {
        public int id; 
        public GameObject lightSource;
        public float warningRadius;
        public float deathRadius;

        public LightSource(GameObject lightSource, Vector2 currentLocation, float warningDistance)
        {
            this.id = lightSource.GetInstanceID();
            this.lightSource = lightSource;
            this.warningRadius = Vector2.Distance(lightSource.transform.position, currentLocation);
            this.deathRadius = this.warningRadius - warningDistance;
        }

        public float RangeToDeath(Vector2 position)
        {
            Vector2 sourcePosition = this.lightSource.transform.position;
            float rawDistance = Vector2.Distance(position, sourcePosition);
            float distance = rawDistance - this.deathRadius;

            return distance;
        }
    }

    private List<LightSource> lightSources = new();
    private SpriteRenderer warningIconSprite;

    // Start is called before the first frame update
    void Start()
    {
        if(this.warningIcon != null)
        {
            this.warningIconSprite = this.warningIcon.GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        // print(this.lightSources.Count);
        if(this.lightSources.Count == 0)
        {
            this.warningIcon.SetActive(false);
            return;
        }

        float distance = this.CalculateMinDistanceToSource();
        if(distance < 0)
        {
            this.Die();
            return;
        }
    }

    float MapValue(float mainValue, float inValueMin, float inValueMax, float outValueMin, float outValueMax)
    {
        // Min-Max Normalization
        return (mainValue - inValueMin) * (outValueMax - outValueMin) / (inValueMax - inValueMin) + outValueMin;
    }

    void SetWarningColor(float distance)
    {
        this.warningIcon.SetActive(true);

        //Convert 0 and 200 distance range to 0f and 1f range
        float lerp = this.MapValue(distance, 0, this.warningDistance, 0f, 1f);
        //Lerp Color between near and far color
        Color lerpColor = Color.Lerp(this.warningColorNear, this.warningColorFar, lerp);

        // this.warningIconSprite.material.color = lerpColor;
        this.warningIconSprite.color = lerpColor;
    }

    void Die()
    {
        if (!invincible)
        {
            this.lightSources.Clear();
            if (this.deathController != null)
            {
                this.deathController.OnDeath();
            }
            if(this.warningIcon != null)
            {
                this.warningIcon.SetActive(false);
            }
        }
    }

    // Return the distance to the closest light source
    float CalculateMinDistanceToSource()
    {
        float min = int.MaxValue;
        this.warningIcon.SetActive(false);

        foreach (LightSource source in this.lightSources)
        {
            // If there is not something in between
            Vector2 sourcePosition = source.lightSource.transform.position;
            if (!Physics2D.Linecast(this.transform.position, sourcePosition))
            {
                float rawDistance = Vector2.Distance(this.transform.position, sourcePosition);
                float distance = source.RangeToDeath(this.transform.position);

                if(distance < min)
                {
                    SetWarningColor(distance);
                    min = distance;
                }
            }
        }
        return min;
    }

    void OnTriggerEnter2D(Collider2D c) //change to 2d for 2d
    {
        if (!c.gameObject.CompareTag("LightExposure") || DeathController.inAnimation) return;

        this.lightSources.Add(new LightSource(c.gameObject, this.transform.position, this.warningDistance));
    }

    void OnTriggerExit2D(Collider2D c) //change to 2d for 2d
    {
        this.lightSources.RemoveAll(s => s.id == c.gameObject.GetInstanceID());
    }
}
