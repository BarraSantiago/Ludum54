using UnityEngine;

public class AttackableObject : MonoBehaviour, Attackable
{
    [SerializeField] HealthBarController healthBarController;

    private float maxHealth = 100;
    [SerializeField] private float health = 100;
    [SerializeField] private Team team = default;

    public float Health { get => health; }
    public float MaxHealth { get => maxHealth; }
    public Team Team { get => team; }
    public bool Alive { get => health > 0; }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public void SetTeam(Team team)
    {
        this.team = team;
    }

    protected virtual void Start()
    {
        if (healthBarController)
        {
            healthBarController.SetAttackableObject(this);
        }
    }

    public virtual void ReceiveDamage(float damage)
    {
        health -= damage;

        if (healthBarController)
        {
            healthBarController.OnHealthChange();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}