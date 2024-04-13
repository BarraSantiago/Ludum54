using UnityEngine;

public class AttackableObject : MonoBehaviour, Attacklable
{
    private float health = 0;

    public void SetMaxHealth(float maxHealth)
    {
        health = maxHealth;
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Dead");
    }
}