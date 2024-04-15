using System;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public bool active = true;
    private void Awake()
    {
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        active = false;
        Time.timeScale = 1f;
    }
}
