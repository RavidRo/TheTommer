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
        protected bool isPossessed = false;

        [SerializeField] private GameObject outline;
        [SerializeField] private LoadingBar loadingBar;

        public bool CanMove { get => canMove; }
        public bool CanCollide { get => canCollide; }
        public bool CanDie { get => canDie; }

        public virtual void Update()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.2f, 1 << LayerMask.NameToLayer("Player"));
            if (this.outline)
            {
                this.outline.SetActive(collider);
            }
        }

        public void LoadPossesion()
        {
            this.loadingBar.gameObject.SetActive(true);
            this.loadingBar.LoadBar();
        }

        public bool IsPossionComplete()
        {
            return this.loadingBar.IsLoaded();
        }

        public bool IsPossessing()
        {
            return this.loadingBar.isActiveAndEnabled && this.loadingBar.IsLoading();
        }

        public void CancelPossession()
        {
            this.loadingBar.Unload();
            this.loadingBar.gameObject.SetActive(false);
        }

        public void OnPossessionGeneric() {
            this.isPossessed = true;
            this.OnPossession();
        }
        public void OnUnpossessionGeneric() {
            this.isPossessed = false;
            this.OnUnpossession();
        }

        public virtual void Interact() { }
        protected virtual void OnPossession() { }
        protected virtual void OnUnpossession() { }
        public virtual void MovementAnimation(float x, float y){ }


    }
    public interface ILightable
    {
        void onWind();

        IEnumerator onTurnOn();
    }
    public interface IDeathSubscriber
    {
        void OnDeath();
    }
}