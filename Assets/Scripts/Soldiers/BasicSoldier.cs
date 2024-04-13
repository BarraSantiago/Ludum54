using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BasicSoldier : MonoBehaviour
{
    public UnitsDataSO unitsData;
    protected AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        OnSoldierInstantiated();
    }

   protected virtual void  OnSoldierInstantiated()
    {
        if (unitsData.invocationSound)
        {
            audioSource.clip = unitsData.invocationSound;
            audioSource.Play();
        }
    }
}
