using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class TeamManager : MonoBehaviour
{    
    public Team team;

    public TowerController mainTower;

    public TowerController[] towers;

    protected float totalHp;

    protected float currentHp;

    [SerializeField] private Image healthBar;
    
    private void Start()
    {
        GameManager.Get().OnTowerReciveHit += GetCurrentHp;
        GameManager.Get().OnTowerReciveHit += UpdateHealthBar;
        totalHp = GetHp();
    }

    public void GetCurrentHp()
    {
        currentHp = GetHp();
    }

    public float GetHp()
    {
        float totalHeal = 0;
        totalHeal += mainTower!= null ? mainTower.Health : 0;
        totalHeal += towers.Sum(tower => tower!=null ? tower.Health : 0);
        return totalHeal;
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHp / totalHp;
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