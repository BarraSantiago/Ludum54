using UnityEngine;

public class ScreenOptions : MonoBehaviour
{
    [SerializeField] private ButtonOption[] buttons = null;
    [SerializeField] private RectTransform selectorTransform = null;

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i]?.Init(OnTriggerMouseEnter);
        }
    }

    private void OnTriggerMouseEnter(RectTransform btnTransform)
    {
        selectorTransform.SetParent(btnTransform);
        selectorTransform.anchoredPosition = Vector3.zero;
    }
}
