using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// Red team will manage the enemy player.
/// </summary>
public class RedTeamManager : TeamManager
{
    private enum RedTeamStates
    {
        Idle,
        Summoning
    }

    [SerializeField] private Transform[] meleeSpawnPoints;
    [SerializeField] private Transform[] rangeSpawnPoints;
    [SerializeField] private UnitsDataSO[] units;

    private RedTeamStates _redTeamState;

    /// <summary>
    /// The next random unit to be summoned.
    /// </summary>
    private int randUnit;

    private void Awake()
    {
        team = Team.RedTeam;
    }

    private void Update()
    {
        ChargeEnergy();
        SpawnBehaviour();
    }

    /// <summary>
    /// State machine that decides 
    /// </summary>
    private void SpawnBehaviour()
    {
        switch (_redTeamState)
        {
            case RedTeamStates.Idle:
                IdleState();
                break;

            case RedTeamStates.Summoning:
                SummonUnit();
                break;
        }
    }

    /// <summary>
    /// When the red team has enough mana, summons a unit.
    /// </summary>
    private void SummonUnit()
    {
        if (units[randUnit].cost > Energy) return;
        
        bool front = units[randUnit].attackType == AttackType.Melee;

        Energy -= units[randUnit].cost;
        
        GameObject unit = Instantiate(units[randUnit].unitRedPrefab);
        
        // TODO change the unit's team to red team
        unit.GetComponent<BasicUnit>().SetUnitData(units[randUnit], team);
        
        Transform smallerRange = front ? meleeSpawnPoints[1] : rangeSpawnPoints[1];
        Transform biggerRange = front ? meleeSpawnPoints[0] : rangeSpawnPoints[0];

        unit.transform.position = SpawnBetweenRange(smallerRange.position, biggerRange.position);
        
        _redTeamState = RedTeamStates.Idle;
    }

    private void IdleState()
    {
        if (Energy > 3)
        {
            _redTeamState = RedTeamStates.Summoning;
            randUnit = Random.Range(0, units.Length);
        }
    }

    /// <summary>
    /// Decides where to spawn the unit
    /// </summary>
    /// <param name="smallerSide"> Vec3 with the smallest X and Y values </param>
    /// <param name="biggerSide"> Vec3 with the biggest X and Y values </param>
    private Vector3 SpawnBetweenRange(Vector3 smallerSide, Vector3 biggerSide)
    {
        int randX = Random.Range((int)smallerSide.x, (int)biggerSide.x);
        int randZ = Random.Range((int)smallerSide.z, (int)biggerSide.z);

        return new Vector3(randX, 0, randZ);
    }
}