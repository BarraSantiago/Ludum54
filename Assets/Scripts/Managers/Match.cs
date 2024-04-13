using UnityEngine;
[System.Serializable]
public class Match
{
    [SerializeField] float matchTime = 60f;

    Timer matchTimer = new Timer();

    bool matchEnded = false;

    public void StartMatch(System.Action onTimeUp)
    {
        matchTimer.Start(() => { onTimeUp?.Invoke(); matchEnded = true; }, matchTime);
    }
    public void Update()
    {
        matchTimer.Update(Time.deltaTime);
    }
    public float GetTimeLeft()
    {
        if (matchEnded)
            return 0;
        return matchTimer.TimeLeft;
    }
}
