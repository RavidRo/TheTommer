using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] GameObject exitSymbol;
    [SerializeField] GameObject player;
    [SerializeField] float minDistanceToExit = 0.5f;
    [SerializeField] string nextSceneName;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(this.player.transform.position, this.transform.position);
        bool closeToPlayer = distanceToPlayer < this.minDistanceToExit;
        if (closeToPlayer && Input.GetButtonDown("Interact"))
        {
            if (this.nextSceneName != null)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
        this.exitSymbol.SetActive(closeToPlayer);
    }
}
