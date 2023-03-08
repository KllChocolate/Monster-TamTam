using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    private bool _IsPlaying = false;
    public AudioClip effectSound;

    private AudioSource audioSource;

    public void Start()
    {
        audioSource= GetComponentInChildren<AudioSource>();
    }
    public void Update()
    {
        if (_IsPlaying == false)
        {
            audioSource.PlayOneShot(effectSound);
            _IsPlaying = true;
        }
    }

}
