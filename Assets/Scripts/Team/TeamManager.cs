using System.Linq;
using UnityEngine;

public abstract class TeamManager : MonoBehaviour
{    
    public Team team;

    public TowerController mainTower;

    public TowerController[] towers;

    public float GetHp()
    {
        float totalHeal = 0;
        totalHeal += mainTower.Health;
        totalHeal += towers.Sum(tower => tower.Health);
        return totalHeal;
    }

    /// <summary>
    /// Currency that lets you summon the unit.
    /// </summary>
    public float Energy { get; protected set; } = 2;
    /// <summary>
    /// Max amount of energy that can be produced
    /// </summary>
    public float MaxEnergy { get; protected set; } = 10;
    
    /// <summary>
    /// Energy gained each second
    /// </summary>
    protected float _energyPerSec = 1;

    protected void ChargeEnergy()
    {
        if (Energy < MaxEnergy)
        {
            Energy += _energyPerSec * Time.deltaTime;
        }
        else
        {
            Energy = MaxEnergy;
        }
    }
}