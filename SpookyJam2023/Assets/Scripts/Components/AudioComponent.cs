using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioComponent : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioClip;

    public void PlayAudio()
    {
        m_AudioClip.Play();
    }
}
