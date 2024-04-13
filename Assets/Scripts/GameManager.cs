using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Match match = new Match();
    [SerializeField] TextMeshProUGUI timerText = null;
    [SerializeField] Button startButton = null;

    public void Start()
    {
        startButton.onClick.AddListener(StartMatch);
    }

    public void StartMatch()
    {
        match.StartMatch(CheckVictory);
    }

    public void CheckVictory()
    {
        //ver quien tiene menos vida
        Debug.Log("CheckVictory");
    }
    public void Update()
    {
        match.Update();
        timerText.text = match.GetTimeLeft().ToString("00");
    }
}

public class Timer
{
    public float TotalTime { get; private set; }

    public float TimeLeft { get; private set; }

    public bool enabled { get; private set; } = false;

    private System.Action OnTimeUp = null;
    public void Start(System.Action OnTimeUp,float totalTime)
    {
        enabled = true;
        this.OnTimeUp = OnTimeUp;
        TotalTime = totalTime;
        TimeLeft = totalTime;
    }
    public void Update(float deltaTime)
    {
        if (!enabled) return; 
        TimeLeft -= deltaTime;
        if (TimeLeft <= 0) { OnTimeUp?.Invoke(); End(); }
    }
    private void End()
    {
        enabled = false;
        TimeLeft = 0;
    }
}
[System.Serializable]
public class Match
{
    [SerializeField] float matchTime = 60f;

    Timer matchTimer = new Timer();

    bool matchEnded = false;

    public void StartMatch(System.Action onTimeUp)
    {
        matchTimer.Start(()=> { onTimeUp?.Invoke(); matchEnded = true; },matchTime);
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
