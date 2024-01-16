using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Intro : MonoBehaviour
{
    public AudioSource[] audioSource;
    void Start()
    {
        StartCoroutine(PlaySoundWithDelay(0, 0.0f));
        StartCoroutine(PlaySoundWithDelay(1, 1.0f));
        StartCoroutine(PlaySoundWithDelay(2, 2.0f));
    }

    IEnumerator PlaySoundWithDelay(int num, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource[num].Play();
    }
}
