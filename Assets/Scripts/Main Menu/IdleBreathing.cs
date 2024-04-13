using UnityEngine;
using System.Collections;

public class IdleBreathing : MonoBehaviour
{
    public float breathsPerMinute = 12f;
    public float swayRange = 10f;
    public float coherenceFactor = 1f;
    public float smoothTime = 0.5f;

    private RectTransform rectTransform;
    private Vector3 startPosition;
    private Vector3 velocity;
    private bool isBreathing = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
        velocity = Vector3.zero;
        StartCoroutine(BreathingSway());
    }

    IEnumerator BreathingSway()
    {
        float breathInterval = 60f / breathsPerMinute;

        while (isBreathing)
        {
            Vector3 targetPosition = startPosition + new Vector3(Random.Range(-swayRange, swayRange), Random.Range(-swayRange, swayRange), 0f);

            while (Vector3.Distance(rectTransform.anchoredPosition, targetPosition) > 0.1f)
            {
                rectTransform.anchoredPosition = Vector3.SmoothDamp(rectTransform.anchoredPosition, targetPosition, ref velocity, smoothTime);
                yield return null;
            }

            rectTransform.anchoredPosition = targetPosition;

            Vector3 oppositeDirection = startPosition + coherenceFactor * (startPosition - targetPosition);

            while (Vector3.Distance(rectTransform.anchoredPosition, oppositeDirection) > 0.1f)
            {
                rectTransform.anchoredPosition = Vector3.SmoothDamp(rectTransform.anchoredPosition, oppositeDirection, ref velocity, smoothTime);
                yield return null;
            }

            rectTransform.anchoredPosition = oppositeDirection;

            yield return new WaitForSeconds(breathInterval);
        }
    }
}