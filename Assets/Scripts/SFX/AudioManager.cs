using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    
    
    public void PlayAudio(int id)
    {
        audioSource.PlayOneShot(audioClips[id]);
    }
}
