using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DialogController : MonoBehaviour
{
    [System.Serializable]
    public struct Sentence
    {
        public string text;
        public SpeachController speachController;
    }

    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private Sentence[] sentences;

    private AudioSource audioSource;
    private int sentenceIndex = 0;

    void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        StartNextSentence();
    }

    private IEnumerator TypeSentence()
    {
        Sentence sentence = sentences[sentenceIndex];
        audioSource.Play();
        sentence.speachController.gameObject.SetActive(true);
        sentence.speachController.startWriting();
        foreach (char letter in sentence.text.ToCharArray())
        {
            sentence.speachController.write(letter);
            yield return new WaitForSeconds(typingSpeed);
        }
        audioSource.Stop();
        sentence.speachController.stopWriting();
    }

    public void StartNextSentence()
    {
        if (sentenceIndex <= sentences.Length && sentenceIndex > 0)
        {
            // Disable last sentence
            Sentence sentence = sentences[sentenceIndex - 1];
            sentence.speachController.gameObject.SetActive(false);
        }

        if (sentenceIndex < sentences.Length)
        {
            StartCoroutine(TypeSentence());
            sentenceIndex++;
        }
    }
}
