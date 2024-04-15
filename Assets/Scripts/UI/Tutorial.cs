using System;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }
}
