using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public Collider PlayerCollider;

    public AudioSource[] InsideAudioSources;

    public AudioSource[] OutsideAudioSources;

    float fadeAmount = 0.01f;

    float audioPitch = 1f;
    float randomPitchModifier = 0f;

    float insideStartingVolume = 0.5f;
    float outsideStartingVolume = 1f;

    void Start()
    {
        foreach (AudioSource audioSource in InsideAudioSources)
        {          
            randomPitchModifier = Random.Range(-0.2f, 0.2f);
            audioSource.pitch = audioPitch + randomPitchModifier;
            audioSource.Play();
        }
        foreach (AudioSource audioSource in OutsideAudioSources)
        {
            randomPitchModifier = Random.Range(-0.2f, 0.2f);
            audioSource.pitch = audioPitch + randomPitchModifier;
            audioSource.Play();
        }
        updateAudioSources(false);
    }

    private void OnTriggerEnter(Collider PlayerCollider)
    {
        updateAudioSources(true);
    }

    private void OnTriggerExit(Collider PlayerCollider)
    {
        updateAudioSources(false);
    }

    private void updateAudioSources(bool inside)
    {
        StopAllCoroutines();

        if (inside)
        {
            foreach (AudioSource audioSource in InsideAudioSources)
            {
                StartCoroutine(FadeAudioIn(audioSource, insideStartingVolume));
            }
            foreach (AudioSource audioSource in OutsideAudioSources)
            {
                StartCoroutine(FadeAudioOut(audioSource));
            }
        }
        else
        {
            foreach (AudioSource audioSource in InsideAudioSources)
            {
                StartCoroutine(FadeAudioOut(audioSource));
            }
            foreach (AudioSource audioSource in OutsideAudioSources)
            {
                StartCoroutine(FadeAudioIn(audioSource, outsideStartingVolume));
            }
        }     

    }

    private IEnumerator FadeAudioIn(AudioSource source, float startingVolume)
    {

        while (source.volume < startingVolume)
        {
            source.volume += fadeAmount;
            yield return null;
        }

    }

    private IEnumerator FadeAudioOut(AudioSource source)
    {

        while (source.volume > 0f)
        {
            source.volume -= fadeAmount;
            yield return null;
        }

    }
}
