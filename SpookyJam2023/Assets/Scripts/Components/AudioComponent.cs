using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioComponent : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioClip;

    public void PlayAudio()
    {
        Debug.Log(m_AudioClip.clip);
        m_AudioClip.Play();
    }
}
