using UnityEngine;

public class TeamManager : MonoBehaviour
{
    /// <summary>
    /// Currency that lets you summon the unit.
    /// </summary>
    public float Energy { get; set; }

    public Team team;

    /// <summary>
    /// Energy gained each second
    /// </summary>
    private float _energyPerSec = 1;
    
    /// <summary>
    /// Max amount of energy that can be produced
    /// </summary>
    public float MaxEnergy { get; private set; }

    private void Awake()
    {
        MaxEnergy = 10;
        Energy = 3;
    }

    private void Update()
    {
        if (Energy < MaxEnergy)
        {
            Energy += _energyPerSec / Time.deltaTime;
        }
        else
        {
            Energy = MaxEnergy;
        }
    }
}