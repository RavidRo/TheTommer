using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightExposure : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] DeathController deathController;
    [SerializeField] GameObject warningIcon;
    [SerializeField] bool invincible = false;

    [SerializeField] Color warningColorNear = Color.red;
    [SerializeField] Color warningColorFar = Color.yellow;

    [SerializeField] float warningDistance = 0.5f;

    private SpriteRenderer warningIconSprite;
    private ContactFilter2D filter = new();

    // Start is called before the first frame update
    void Start()
    {
        if (this.warningIcon != null)
        {
            this.warningIconSprite = this.warningIcon.GetComponent<SpriteRenderer>();
        }

        filter.SetLayerMask(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Light")));
        filter.useTriggers = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (DeathController.inAnimation) return;
        if (this.warningIcon != null)
        {
            this.warningIcon.SetActive(false);
        }

        Collider2D[] overlapedColliders = new Collider2D[10];
        GameObject me = player.possessing ? player.possessing.gameObject : player.Tommer.gameObject;
        int numOfResults = me.GetComponent<Collider2D>().OverlapCollider(filter, overlapedColliders);

        float minDistance = float.MaxValue;
        int numOfLights = 0;
        for (int i = 0; i < numOfResults; i++)
        {
            Collider2D collider = overlapedColliders[i];

            // If there is not something in between
            Vector2 lightSourcePosition = collider.transform.position;
            if (!Physics2D.Linecast(this.transform.position, lightSourcePosition, LayerMask.GetMask("Walls")))
            {
                numOfLights++;
                if (collider.GetType() == typeof(CircleCollider2D))
                {
                    float warningRadius = ((CircleCollider2D)collider).radius / 2;
                    float distance = Vector2.Distance(this.transform.position, lightSourcePosition);
                    float traveled = warningRadius - distance;
                    if (traveled > warningDistance)
                    {
                        Die();
                        return;
                    }
                    minDistance = Mathf.Min(minDistance, warningDistance - traveled);
                }
                else
                {
                    Debug.LogWarning("Collided with light that is not circle coliider");
                }
            }
        }

        if(numOfLights > 0)
        {
            SetWarningColor(minDistance);
        }
    }

    private void Die()
    {
        if (!invincible)
        {
            if (this.deathController != null)
            {
                this.deathController.OnDeath();
            }
        }
    }

    private float MapValue(float mainValue, float inValueMin, float inValueMax, float outValueMin, float outValueMax)
    {
        // Min-Max Normalization
        return (mainValue - inValueMin) * (outValueMax - outValueMin) / (inValueMax - inValueMin) + outValueMin;
    }

    private void SetWarningColor(float distance)
    {
        this.warningIcon.SetActive(true);

        //Convert 0 and 200 distance range to 0f and 1f range
        float lerp = this.MapValue(distance, 0, this.warningDistance, 0f, 1f);
        //Lerp Color between near and far color
        Color lerpColor = Color.Lerp(this.warningColorNear, this.warningColorFar, lerp);

        // this.warningIconSprite.material.color = lerpColor;
        this.warningIconSprite.color = lerpColor;
    }
}
