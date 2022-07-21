using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeachController : MonoBehaviour
{
    [SerializeField] float typingSpeed = 0.05f;
    [SerializeField] TextMeshProUGUI speachText;
    [SerializeField] string[] sentences;
    [SerializeField] GameObject continueButton;

    private int sentenceIndex = 0;

    void Start()
    {
        StartNextSentence();
    }

    private IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[sentenceIndex].ToCharArray())
        {
            speachText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        continueButton.SetActive(true);
    }

    public void StartNextSentence()
    {
        continueButton.SetActive(false);
        if (sentenceIndex >= sentences.Length)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(TypeSentence());
            sentenceIndex++;
        }
    }
}
