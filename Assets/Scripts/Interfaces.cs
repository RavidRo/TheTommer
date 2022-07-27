using UnityEngine;
using System.Collections;

namespace interfaces
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class IPossessable : MonoBehaviour
    {
        protected bool canMove = false;
        protected bool canCollide = false;
        protected bool canDie = false;

        [SerializeField] private GameObject outline;

        public bool CanMove { get => canMove; }
        public bool CanCollide { get => canCollide; }

        public virtual void Update()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.2f, 1 << LayerMask.NameToLayer("Player"));
            if (this.outline)
            {
                this.outline.SetActive(collider);
            }
        }

        public void OnPossessionGeneric() {
            if (this.canDie)
            {
                this.GetComponent<LightExposure>().enabled = true;
            }
            this.OnPossession();
        }
        public void OnUnpossessionGeneric() {
            if (this.canDie)
            {
                this.GetComponent<LightExposure>().enabled = false;
            }
            this.OnUnpossession();
        }

        public virtual void Interact() { }
        protected virtual void OnPossession() { }
        protected virtual void OnUnpossession() { }
        public virtual void MovementAnimation(float x, float y){ }


    }

    public interface IDeathSubscriber
    {
        void OnDeath();
    }
}