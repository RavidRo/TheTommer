using UnityEngine;
using System.Collections;

namespace interfaces
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class IPossessable : MonoBehaviour
    {
        protected bool canMove = false;
        protected bool canCollide = false;

        public bool CanMove { get => canMove; }
        public bool CanCollide { get => canCollide; }

        public virtual void Interact() { }
        public virtual void OnPossession() { }
        public virtual void OnUnpossession() { }
        public virtual void MovementAnimation(float x, float y){ }
    }

    public interface IDeathSubscriber
    {
        void OnDeath();
    }
}