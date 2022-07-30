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

    [System.Serializable]
    public struct Dialog
    {
        public string name;
        public Sentence[] sentences;
    }

    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private Dialog[] dialogs;

    private AudioSource audioSource;
    private int sentenceIndex = 0;
    private int dialogIndex = -1;

    void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
    }

    private IEnumerator TypeSentence()
    {
        Sentence sentence = dialogs[dialogIndex].sentences[sentenceIndex];
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

    public void StartNextDialog()
    {
        dialogIndex++;
        sentenceIndex = 0;
        StartNextSentence();
    }

    public void StartNextSentence()
    {
        if (sentenceIndex <= dialogs[dialogIndex].sentences.Length && sentenceIndex > 0)
        {
            // Disable last sentence
            Sentence sentence = dialogs[dialogIndex].sentences[sentenceIndex - 1];
            sentence.speachController.gameObject.SetActive(false);
        }

        if (sentenceIndex < dialogs[dialogIndex].sentences.Length)
        {
            StartCoroutine(TypeSentence());
            sentenceIndex++;
        }
    }
}
