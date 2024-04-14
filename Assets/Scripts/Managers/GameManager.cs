using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public Action OnDestroyMainTower;
    public Action OnDestroyNormalTower;

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
        if (timerText)
        timerText.text = match.GetTimeLeft().ToString("00");
    }
}

