using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameplayUI : MonoBehaviour
{
    [Header("Main Configuration")]
    [SerializeField] private GameObject holder = null;
    [SerializeField] private Button retryBtn = null;
    [SerializeField] private Button menuBtn = null;

    [Header("Win Configuration")]
    [SerializeField] private GameObject winTextGO = null;

    [Header("Lose Configuration")]
    [SerializeField] private GameObject loseTextGO = null;

    private void Start()
    {
        retryBtn.onClick.AddListener(RetryGame);
        menuBtn.onClick.AddListener(BackToMenu);
    }

    public void ToggleUI(bool win)
    {
        holder.SetActive(true);

        if (win)
        {
            winTextGO.SetActive(true);
            loseTextGO.SetActive(false);
        }
        else
        {
            loseTextGO.SetActive(true);
            winTextGO.SetActive(false);
        }
    }

    private void RetryGame()
    {
        SceneManager.LoadScene(1);
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
