public class Timer
{
    public float TotalTime { get; private set; }

    public float TimeLeft { get; private set; }

    public bool enabled { get; private set; } = false;

    private System.Action OnTimeUp = null;
    public void Start(System.Action OnTimeUp, float totalTime)
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