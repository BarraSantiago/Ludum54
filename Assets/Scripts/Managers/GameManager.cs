using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Utils;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public Action OnDestroyMainTower;
    public Action OnDestroyNormalTower;
    public Action OnTowerReciveHit;

    [SerializeField] Match match = new Match();
    [SerializeField] TextMeshProUGUI timerText = null;
    [SerializeField] Button startButton = null;

    public void Start()
    {
        StartMatch();
        match.onEndGame += end;
    }

    private void end()
    {
        Debug.Log("End");
    }

    public void StartMatch()
    {
        match.StartMatch();
    }
    
    public void CheckVictory()
    {
        //ver quien tiene menos vida
        Debug.Log("CheckVictory");
    }

    public void Update()
    {
        match.Update();
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        int time = Mathf.RoundToInt(match.GetTimeLeft());
        int minutes = (int)(time / 60f);
        int seconds = (int)(time % 60f);

        timerText.text = minutes + ":";
        timerText.text += seconds < 10 ? "0" + seconds : seconds.ToString();
    }
}