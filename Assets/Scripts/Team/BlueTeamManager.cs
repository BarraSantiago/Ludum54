using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Blue team represents the player's team.
/// </summary>
public class BlueTeamManager : TeamManager
{
    [SerializeField] private Image energyBar;
    public System.Action OnLose = null;

    private UnitCard[] unitCards = null;

    private void Awake()
    {
        team = Team.BlueTeam;
        energyBar.fillAmount = 1f;

        unitCards = FindObjectsOfType<UnitCard>();

        for (int i = 0; i < unitCards.Length; i++)
        {
            unitCards[i].onBuyItem = (cost) => 
            { 
                Energy -= cost; 
            };
        }
    }
    
    private void Update()
    {
        ChargeEnergy();
        energyBar.fillAmount = Energy * MaxEnergy / 100f;

        for (int i = 0; i < unitCards.Length; i++)
        {
            unitCards[i].UpdateStateByEnergyLeft((int)Energy);
        }
    }
}