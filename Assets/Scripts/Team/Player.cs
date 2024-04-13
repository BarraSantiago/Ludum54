using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TeamManager teamManager;
    [SerializeField] private Slider energySlider;
    public System.Action OnMorir = null;

    private void Start()
    {
        teamManager.team = Team.BlueTeam;
        energySlider.maxValue = teamManager.MaxEnergy;
        energySlider.minValue = 0;
    }

    private void Update()
    {
        energySlider.value = teamManager.Energy;
    }

    public float GetVida()
    {
        return 1;
    }
}