using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class BasicUnit : AttackableObject
{
    public enum State
    {
        FindingTarget,
        PursuingTarget,
        Attacking
    }

    AnimationStateController animationStateController;
    GameManager gm;
    [SerializeField] Transform spawnProyectilePostion;
    [SerializeField] GameObject spawnProyectileParticles;
    [SerializeField] GameObject projectile;

    #region PROTECTED_FIELDS

    public UnitsDataSO unitData;
    protected AudioSource audioSource;
    [SerializeField] protected AttackableObject target;

    #endregion

    #region PRIVATE_FIELDS

    private NavMeshAgent agent = null;

    private State currentState = default;
    private float attackCooldown = 0;
    private bool noMoreEnemies = false; // when the game ends

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        animationStateController = GetComponent<AnimationStateController>();
    }

    protected override void Start()
    {
        //debug, this should be set by an outside system
        OnUnitInstantiated();
        base.Start();

        gm = GameManager.Get();
        gm.OnDestroyNormalTower += () => currentState = State.FindingTarget;
    }

    private void FixedUpdate()
    {
        if (noMoreEnemies)
        {
            Debug.Log("No more enemies");
            return;
        }

        if (target)
        {
            agent.SetDestination(target.transform.position);
        }
        recalculateTarget();
        UpdateState();
    }

    #endregion

    #region INITIALIZATION

    public void SetUnitData(UnitsDataSO data, Team team)
    {
        unitData = data;
        SetMaxHealth(data.maxHealth);
        currentState = State.FindingTarget;
        agent.speed = unitData.movementSpeed;
        SetTeam(team);
    }

    protected virtual void OnUnitInstantiated()
    {
        if (unitData.invocationSound)
        {
            audioSource.clip = unitData.invocationSound;
            audioSource.Play();
        }
    }

    #endregion

    #region PROTECTED_METHODS

    private void recalculateTarget()
    {
        AttackableObject[] allEntities = FindObjectsOfType<AttackableObject>(false);
        for (int i = 0; i < allEntities.Length; i++)
        {
            if (allEntities[i].Team != Team && allEntities[i].Alive && allEntities[i] != this)
            {
                if (target == null)
                {
                    target = allEntities[i];
                }
                else
                {
                    if (Vector3.Distance(allEntities[i].transform.position, transform.position) <
                        Vector3.Distance(target.transform.position, transform.position))
                    {
                        target = allEntities[i];
                    }
                }
            }
        }
    }

    protected virtual void FindTarget()
    {
        if (!TargetIsValid())
        {
            AttackableObject[] allEntities = FindObjectsOfType<AttackableObject>(false);
            List<AttackableObject> enemies = new List<AttackableObject>();

            for (int i = 0; i < allEntities.Length; i++)
            {
                if (allEntities[i].Team != Team && allEntities[i].Alive && allEntities[i] != this)
                {
                    enemies.Add(allEntities[i]);

                    Debug.Log(allEntities[i].name);
                }
            }

            if (enemies.Count == 0)
            {
                noMoreEnemies = true;
                return;
            }

            AttackableObject closerEnemy = enemies[0];

            float closerDistance = Vector3.Distance(closerEnemy.transform.position, transform.position);

            for (int i = 1; i < enemies.Count; i++)
            {
                float distance = Vector3.Distance(enemies[i].transform.position, transform.position);
                if (distance < closerDistance && enemies[i] != this)
                {
                    closerDistance = distance;
                    closerEnemy = enemies[i];
                }
            }

            target = closerEnemy;
        }


        currentState = State.PursuingTarget;

        //Debug.Log("Find");
    }

    protected virtual void PursueTarget()
    {
        if (TargetIsClose())
        {
            agent.isStopped = true;
            animationStateController.SetMoveEndAnimation(true);
            currentState = State.Attacking;
        }
        else
        {
            agent.isStopped = false;
        }

        //Debug.Log("Pursue");
    }

    protected virtual void Attack()
    {
        if (!TargetIsValid())
        {
            currentState = State.FindingTarget;
            return;
        }

        if (!TargetIsClose())
        {
            currentState = State.PursuingTarget;
            return;
        }

        animationStateController.SetAttackAnimation(true);


        if (attackCooldown <= 0)
        {
            attackCooldown = unitData.attackSpeed;

            Hit();
        }
    }

    protected virtual void Hit()
    {
        if (unitData.attackSound)
        {
            audioSource.clip = unitData.attackSound;
            audioSource.Play();
        }

        switch (unitData.attackType)
        {
            case AttackType.Melee:
                target.ReceiveDamage(unitData.damage);
                break;
            case AttackType.Range:
                GameObject proj = Instantiate(projectile.gameObject, spawnProyectilePostion.position,
                    spawnProyectilePostion.rotation);
                Instantiate(spawnProyectileParticles, spawnProyectilePostion.position, spawnProyectilePostion.rotation);
                BasicProjectile basicProjectile = proj.GetComponent<BasicProjectile>();

                basicProjectile.SetTarget(target);
                basicProjectile.SetDamage(unitData.damage);
                break;
            case AttackType.Bomb:

                Collider[] colliders = Physics.OverlapSphere(transform.position, unitData.range);

                foreach (Collider coll in colliders)
                {
                    if (coll.TryGetComponent(out AttackableObject targets))
                    {
                        if (target.Team != team)
                        {
                            target.ReceiveDamage(unitData.damage);
                        }
                    }
                }

                Destroy(gameObject);
                //Hay que instanciar alguna explosion


                break;
            default:
                break;
        }
    }

    #endregion

    #region PRIVATE_METHODS

    private void UpdateState()
    {
        switch (currentState)
        {
            case State.FindingTarget:

                FindTarget();

                break;

            case State.PursuingTarget:

                animationStateController.SetMoveAnimation(true);
                PursueTarget();

                break;
            case State.Attacking:

                Attack();

                break;
            default:
                break;
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.fixedDeltaTime;
        }
    }

    private bool TargetIsClose()
    {
        return Vector3.Distance(target.transform.position, transform.position) < unitData.range;
    }

    private bool TargetIsValid()
    {
        return target != null && target.gameObject.activeInHierarchy;
    }

    public override void Die()
    {
        if (unitData.deathSound)
        {
            audioSource.clip = unitData.deathSound;
            audioSource.Play();
        }

        animationStateController.SetDieAnimation(true);

        base.Die();
    }
    #endregion
   
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, unitData.range);
    }
}