using UnityEngine;
[System.Serializable]
public class Match
{
    [SerializeField] BlueTeamManager player1 = null;

    [SerializeField] BlueTeamManager player2 = null;

    [SerializeField] BlueTeamManager winner = null;

    [SerializeField] float matchTime = 60f;

    Timer matchTimer = new Timer();

    bool matchEnded = false;

    public System.Action onEndGame = null;

    public void StartMatch()
    {
        matchTimer.Start(EndMatch, matchTime);
        player1.OnLose += EndMatch;
        player2.OnLose += EndMatch;
    }
    public virtual void EndMatch()
    {
        winner = CheckPlayerWinner();
        matchEnded = true;
        onEndGame?.Invoke();
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
    public BlueTeamManager CheckPlayerWinner()
    {
        return player1.GetHp() > player2.GetHp() ? player1 : player2;
    }
    public void WinnerPlayer(BlueTeamManager blueTeamManager)
    {
        Debug.Log("WinnerPlayer: " + blueTeamManager.name);
    }
}
