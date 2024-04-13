using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject holder = null;
    [SerializeField] private Button continueBtn = null;
    [SerializeField] private Button menuBtn = null;

    private void Start()
    {
        continueBtn.onClick.AddListener(Continue);
        menuBtn.onClick.AddListener(BackToMenu);
    }

    public void TogglePause(bool status)
    {
        Time.timeScale = status ? 0f : 1f;
        holder.SetActive(status);
    }

    private void Continue()
    {
        TogglePause(false);
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
