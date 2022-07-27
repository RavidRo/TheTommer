using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertPopup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float popUpTime = 1f;  
    
    public IEnumerator onPopUp(){
        this.gameObject.SetActive(true);
        yield return new WaitForSeconds(popUpTime);
        this.gameObject.SetActive(false);
    }
}
