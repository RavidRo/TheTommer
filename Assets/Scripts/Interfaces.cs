using UnityEngine;
using System.Collections;

namespace interfaces
{
    public interface IPossessable
    {
        void interact();
        void movementAnimation(float x, float y);
        void onPossession();
        void onUnpossession();
    }

    public interface IDeathSubscriber
    {
        void OnDeath();
    }
}