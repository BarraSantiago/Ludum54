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

    List<AttackableObject> targets;
    float counter;

    private void Start()
    {
        gm = GameManager.Get();
        targets = new List<AttackableObject>();

        SetTeam(team);
        SetMaxHealth(maxHealth);
    }

    public override void Die()
    {
        (isMainTower ? gm.OnDestroyMainTower : gm.OnDestroyNormalTower)?.Invoke();

        //Animacion de destruccion del modelo
        model.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AttackableObject target))
        {
            if (target.Team != team)
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
           
            //Aca se tiene que instanciar un proyectil que vaya hacia el targets[0] y el proyectil haga el ReciveDamage;

            if (targets[0].gameObject.activeInHierarchy) 
            {
                targets[0].ReceiveDamage(damage);
            }
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

