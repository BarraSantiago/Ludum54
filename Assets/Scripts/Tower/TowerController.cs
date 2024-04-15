using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : AttackableObject
{
    GameManager gm;

    [SerializeField] float towerMaxHealth;
    [SerializeField] GameObject basicProjectile;
    [SerializeField] GameObject spawnProjectileParticles;

    [SerializeField] Transform spawnProjectilePosition;
    [SerializeField] Transform modelTower;
    [SerializeField] Team towerTeam;

    [SerializeField] float damage;
    [SerializeField] float attackSpeed;

    [SerializeField] bool isMainTower;
    [HideInInspector] public bool towerIsAlive;

    List<AttackableObject> targets;
    float counter;

    protected override void Start()
    {
        base.Start();

        gm = GameManager.Get();
        targets = new List<AttackableObject>();

        towerIsAlive = true;

        SetTeam(towerTeam);
        SetMaxHealth(towerMaxHealth);
    }

    public override void ReceiveDamage(float damage)
    {
        base.ReceiveDamage(damage);

        gm.OnTowerReciveHit?.Invoke();
    }

    public override void Die()
    {
        (isMainTower ? gm.OnDestroyMainTower : gm.OnDestroyNormalTower)?.Invoke();

        OnDie?.Invoke();

        //Animacion de destruccion del modelo

        modelTower.gameObject.SetActive(false);
        this.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AttackableObject target))
        {
            if (target.Team != towerTeam)
            {
                targets.Add(target);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out AttackableObject target))
        {
            if (targets.Contains(target))
            {
                targets.Remove(target);
            }
        }
    }

    private void Update()
    {
        ClearTargetsList();

        if (targets.Count <= 0 || !towerIsAlive)
        {
            return;
        }

        counter += Time.deltaTime;
        
        if (targets[0] != null)
            modelTower.transform.LookAt(targets[0].transform);

        if (counter >= attackSpeed)
        {
            counter = 0;

            GameObject projectile = Instantiate(basicProjectile, spawnProjectilePosition.transform.position, spawnProjectilePosition.transform.rotation);
            Instantiate(spawnProjectileParticles, spawnProjectilePosition.transform.position, spawnProjectilePosition.transform.rotation);

            BasicProjectile proj = projectile.GetComponent<BasicProjectile>();

            proj.Initialize(targets[0], damage);
        }
    }

    void ClearTargetsList()
    {
        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if (!targets[i].Alive)
            {
                targets.RemoveAt(i);
            }
        }
    }
}

