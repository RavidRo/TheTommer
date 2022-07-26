using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeachController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speachText;
    [SerializeField] GameObject continueButton;
    //[SerializeField] float typingSpeed = 0.05f;
    //[SerializeField] string[] sentences;
    //[SerializeField] AudioSource audioSource;



    //private int sentenceIndex = 0;

    //void Start()
    //{
    //    StartNextSentence();
    //}

    //private IEnumerator TypeSentence()
    //{
    //    audioSource.Play();
    //    foreach (char letter in sentences[sentenceIndex].ToCharArray())
    //    {
    //        speachText.text += letter;
    //        yield return new WaitForSeconds(typingSpeed);
    //    }
    //    audioSource.Stop();
    //    continueButton.SetActive(true);
    //}

    //public void StartNextSentence()
    //{
    //    continueButton.SetActive(false);
    //    if (sentenceIndex >= sentences.Length)
    //    {
    //        this.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        StartCoroutine(TypeSentence());
    //        sentenceIndex++;
    //    }
    //}
    public void OnEnable()
    {
        continueButton.SetActive(false);
    }

    public void startWriting()
    {

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
