using System;

using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonOption : MonoBehaviour, IPointerEnterHandler
{
    private RectTransform rectTransform = null;
    private Action<RectTransform> onPointerEnter = null;

    private void Awake()
    {
        rectTransform = (RectTransform)transform.GetChild(0).transform;
    }

    public void Init(Action<RectTransform> onPointerEnter)
    {
        this.onPointerEnter = onPointerEnter;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke(rectTransform);
    }
}
