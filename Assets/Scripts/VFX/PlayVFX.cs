using UnityEngine;

public class PlayVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps = null;

    public void PlayPS()
    {
        ps.Play();
    }
}