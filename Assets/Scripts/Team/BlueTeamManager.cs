using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Blue team represents the player's team.
/// </summary>
public class BlueTeamManager : TeamManager
{
    [SerializeField] private Slider energySlider;
    [SerializeField] private TextMeshProUGUI txtEnergy = null;
    public System.Action OnLose = null;

    private UnitCard[] unitCards = null;

    private void Awake()
    {
        team = Team.BlueTeam;
        energySlider.maxValue = MaxEnergy;
        energySlider.minValue = 0;

        unitCards = FindObjectsOfType<UnitCard>();

        for (int i = 0; i < unitCards.Length; i++)
        {
            unitCards[i].onBuyItem = (cost) => { Energy -= cost; };
        }
    }
    
    private void Update()
    {
        ChargeEnergy();
        energySlider.value = Energy;
        txtEnergy.text = ((int)Energy).ToString();

        for (int i = 0; i < unitCards.Length; i++)
        {
            unitCards[i].UpdateStateByEnergyLeft((int)Energy);
        }
    }
}