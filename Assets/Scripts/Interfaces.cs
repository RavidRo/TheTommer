using UnityEngine;
using System.Collections;

namespace interfaces
{
    public interface IPossessable
    {
        void interact();
        void movementAnimation(float x, float y);
    }

    public interface IDeathSubscriber
    {
        void OnDeath();
    }
}