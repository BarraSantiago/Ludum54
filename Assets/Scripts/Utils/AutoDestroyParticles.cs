using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticles : MonoBehaviour
{
    [SerializeField] private float destroyTime = 1f;

    private void Start()
    {
        Destroy(gameObject, destroyTime * Time.timeScale);
    }
}
