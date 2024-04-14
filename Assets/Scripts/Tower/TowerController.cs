using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : AttackableObject
{
    GameManager gm;

    [SerializeField] float towerMaxHealth;
    [SerializeField] GameObject basicProjectile;
    [SerializeField] Transform spawnProjectilePosition;
    [SerializeField] Transform model;
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

        SetTeam(towerTeam);
        SetMaxHealth(towerMaxHealth);
    }

    public override void Die()
    {
        (isMainTower ? gm.OnDestroyMainTower : gm.OnDestroyNormalTower)?.Invoke();

        //Animacion de destruccion del modelo
        this.enabled = false;
        model.gameObject.SetActive(false);
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

    private void Update()
    {
        ClearTargetsList();

        if (targets.Count <= 0 || !towerIsAlive)
        {
            return;
        }

        counter += Time.deltaTime;

        if (counter >= attackSpeed)
        {
            counter = 0;

            GameObject projectile = Instantiate(basicProjectile, spawnProjectilePosition.transform.position, spawnProjectilePosition.transform.rotation);
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

