using UnityEngine;
using System.Collections;
namespace interfaces
{
    public interface IAction{
        void action();
    }

    public interface IDeathSubscriber
    {
        void OnDeath();
    }
}