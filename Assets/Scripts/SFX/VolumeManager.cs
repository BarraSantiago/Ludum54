using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject onSprite;
    [SerializeField] private GameObject offSprite;

    private bool status = true;

    public void ToggleVolume()
    {
        status = !status;
        const string volumeName = "Volume";
        onSprite.SetActive(status);
        offSprite.SetActive(!status);
        audioMixer.SetFloat(volumeName, status ? 0 : -80);
    }
}
