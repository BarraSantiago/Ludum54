using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Blue team represents the player's team.
/// </summary>
public class BlueTeamManager : TeamManager
{
    [SerializeField] private Slider energySlider;
    public System.Action OnLose = null;
    private void Awake()
    {
        team = Team.BlueTeam;
        energySlider.maxValue = MaxEnergy;
        energySlider.minValue = 0;
    }
    
    private void Update()
    {
        ChargeEnergy();
        energySlider.value = Energy;
    }
    
    public float GetHp()
    {
        return 1;
    }
}