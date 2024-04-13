using UnityEngine;
[System.Serializable]
public class Match
{
    [SerializeField] Player player1 = null;

    [SerializeField] Player player2 = null;

    [SerializeField] Player winner = null;

    [SerializeField] float matchTime = 60f;

    Timer matchTimer = new Timer();

    bool matchEnded = false;

    public System.Action onEndGame = null;

    public void StartMatch()
    {
        matchTimer.Start(EndMatch, matchTime);
        player1.OnMorir += EndMatch;
        player2.OnMorir += EndMatch;
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
    public Player CheckPlayerWinner()
    {
        return player1.GetVida() > player2.GetVida() ? player1 : player2;
    }
    public void WinnerPlayer(Player player)
    {
        Debug.Log("WinnerPlayer: " + player.name);
    }
}
