using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneController : MonoBehaviour
{
    [SerializeField] private TommerController tommer;

    void Start()
    {
        tommer.EndingScene();
    }
}
