using UnityEngine;
[System.Serializable]
public class Match
{
    [SerializeField] BlueTeamManager player1 = null;

    [SerializeField] RedTeamManager player_bot = null;

    [SerializeField] float matchTime = 60f;

    Timer matchTimer = new Timer();

    bool matchEnded = false;

    public System.Action onEndGame = null;

    public void StartMatch()
    {
        matchTimer.Start(EndMatch, matchTime);
        player1.OnLose += EndMatch;
        player1.mainTower.OnDie += EndMatch;
        player_bot.mainTower.OnDie += EndMatch;
        //player_bot.OnLose += EndMatch;
    }
    public virtual void EndMatch()
    {
        bool winner = CheckPlayerWinner();
        matchEnded = true;
        onEndGame?.Invoke();
        if (winner)
        {
            WinnerPlayer();
        }
        else
        {
            Debug.Log("Player lose");
        }
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
    public bool CheckPlayerWinner()
    {
        return player1.GetHp() > player_bot. GetHp() ? true : false;
    }
    public void WinnerPlayer()
    {
        Debug.Log("Player Winn");
    }
}
