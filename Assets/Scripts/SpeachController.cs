using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeachController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speachText;
    [SerializeField] GameObject continueButton;

    public void OnEnable()
    {
        continueButton.SetActive(false);
    }

    public void startWriting()
    {
        speachText.text = "";
    }

    public void write(char letter)
    {
        speachText.text += letter;
    }

    public void stopWriting()
    {
        continueButton.SetActive(true);
    }
}
