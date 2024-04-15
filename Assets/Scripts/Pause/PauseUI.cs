using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject holder = null;
    [SerializeField] private Button continueBtn = null;
    [SerializeField] private Button retryBtn = null;
    [SerializeField] private Button menuBtn = null;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Tutorial tutorial;
    private void Start()
    {
        continueBtn.onClick.AddListener(Continue);
        retryBtn.onClick.AddListener(Retry);
        menuBtn.onClick.AddListener(BackToMenu);
    }

    public void TogglePause(bool status)
    {
        if (!status)
        {
            if (!tutorial.active)
            {
                Time.timeScale = 1;
            }
        }
        else
        {
            Time.timeScale = 0;
        }
        holder.SetActive(status);
    }

    
    
    private void Continue()
    {
        TogglePause(false);
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Retry()
    {
        SceneManager.LoadScene(1);
    }
}