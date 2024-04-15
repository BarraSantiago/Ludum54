using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField] GameObject impactParticles;
    AttackableObject target;
   [SerializeField] float speed = 5f;
    float damage = 0;
    float range = 0.5f; //Distancia del objetivo a la explotan (para pegarles al costado y no adentro)
    public void Initialize(AttackableObject attackableObject, float Damage)
    {
        target = attackableObject;
        damage = Damage;
    }

    public void SetTarget(AttackableObject attackableObject)
    {
        target = attackableObject;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.transform.position) < range)
            {
                Attack();
                Instantiate(impactParticles, transform.position, transform.rotation);
            }
        }
        else
        {
            // Si el objetivo ya no existe, destruye el proyectil
            Destroy(gameObject);
        }
    }

    protected virtual void Attack()
    {
        target.ReceiveDamage(damage);

        Destroy(gameObject);
    }
}
