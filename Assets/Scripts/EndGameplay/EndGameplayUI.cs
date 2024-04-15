using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class EndGameplayUI : MonoBehaviour
{
    [Header("Main Configuration")]
    [SerializeField] private GameObject holder = null;
    [SerializeField] private RectTransform holderButtonsTransform = null;
    [SerializeField] private Image backgroundImage = null;
    [SerializeField] private Button retryBtn = null;
    [SerializeField] private Button menuBtn = null;
    [SerializeField] private Button exitBtn = null;
    [SerializeField] private TMP_Text selectorText = null;

    [Header("Win Configuration")]
    [SerializeField] private Sprite winSprite = null;
    [SerializeField] private Color winColor = Color.white;
    [SerializeField] private Vector2 winButtonPosition = Vector2.zero;

    [SerializeField] private Sprite retryWinSprite = null;
    [SerializeField] private Sprite menuWinSprite = null;
    [SerializeField] private Sprite exitWinSprite = null;

    [SerializeField] private Sprite retryPressedWinSprite = null;
    [SerializeField] private Sprite menuPressedWinSprite = null;
    [SerializeField] private Sprite exitPressedWinSprite = null;

    [Header("Lose Configuration")]
    [SerializeField] private Sprite loseSprite = null;
    [SerializeField] private GameObject loseTextGO = null;
    [SerializeField] private Color loseColor = Color.white;
    [SerializeField] private Vector2 loseButtonPosition = Vector2.zero;

    [SerializeField] private Sprite retryLoseSprite = null;
    [SerializeField] private Sprite menuLoseSprite = null;
    [SerializeField] private Sprite exitLoseSprite = null;

    [SerializeField] private Sprite retryPressedLoseSprite = null;
    [SerializeField] private Sprite menuPressedLoseSprite = null;
    [SerializeField] private Sprite exitPressedLoseSprite = null;

    private void Start()
    {
        retryBtn.onClick.AddListener(RetryGame);
        menuBtn.onClick.AddListener(BackToMenu);
        exitBtn.onClick.AddListener(ExitGame);
        Match.onGameOutcome += ToggleUI;
    }

    private void OnDestroy()
    {
        Match.onGameOutcome -= ToggleUI;
    }

    public void ToggleUI(bool win)
    {
        holder.SetActive(true);
        backgroundImage.sprite = win ? winSprite : loseSprite;

        retryBtn.image.sprite = win ? retryWinSprite : retryLoseSprite;
        menuBtn.image.sprite = win ? menuWinSprite : menuLoseSprite;
        exitBtn.image.sprite = win ? exitWinSprite : exitLoseSprite;

        SetPressedSprite(retryBtn, win ? retryPressedWinSprite : retryPressedLoseSprite);
        SetPressedSprite(menuBtn, win ? menuPressedWinSprite : menuPressedLoseSprite);
        SetPressedSprite(exitBtn, win ? exitPressedWinSprite : exitPressedLoseSprite);

        selectorText.color = win ? winColor : loseColor;

        if (win)
        {
            holderButtonsTransform.anchoredPosition = winButtonPosition;
            loseTextGO.SetActive(false);
        }
        else
        {
            holderButtonsTransform.anchoredPosition = loseButtonPosition;
            loseTextGO.SetActive(true);
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
    
    private void ExitGame()
    {
        Application.Quit();
    }

    private void SetPressedSprite(Button btn, Sprite sprite)
    {
        SpriteState spriteState = btn.spriteState;
        spriteState.pressedSprite = sprite;
        btn.spriteState = spriteState;
    }
}
