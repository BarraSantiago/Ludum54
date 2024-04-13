using UnityEngine;

public class AttackableObject : MonoBehaviour, Attackable
{
    private float health = 0;

    public void SetMaxHealth(float maxHealth)
    {
        health = maxHealth;
    }

    public virtual void ReceiveDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("Dead");
    }
}