using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : AttackableObject
{
    GameManager gm;

    [SerializeField] float maxHealth;
    [SerializeField] Transform model;
    [SerializeField] Team team;

    [SerializeField] float damage;
    [SerializeField] float attackSpeed;

    [SerializeField] bool isMainTower;
    [HideInInspector] public bool towerIsAlive;

    List<ITarget> targets;
    float counter;

    private void Start()
    {
        gm = GameManager.Get();
        targets = new List<ITarget>();

        SetMaxHealth(maxHealth);
    }

    public void Dies()
    {
        (isMainTower ? gm.OnDestroyMainTower : gm.OnDestroyNormalTower)?.Invoke();

        //Animacion de destruccion del modelo
        model.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITarget target))
        {
            if (target.GetTeam() != team)
            {
                targets.Add(target);
            }
        }
    }

    private void Update()
    {
        if (targets.Count <= 0 || !towerIsAlive)
        {
            return;
        }

        counter += Time.deltaTime;

        if (counter >= attackSpeed)
        {
            counter = 0;

            foreach (ITarget target in targets)
            {
                if (!target.isValid())
                {
                    targets.Remove(target);
                }
            }

            if (targets.Count > 0)
            {
                targets[0].ReceiveDamage(damage);
            }
        }
    }

    public Team GetTeam()
    {
        return team;
    }

    public Transform GetTransform()
    {
        return model;
    }

    public bool isValid()
    {
        return towerIsAlive;
    }
}

