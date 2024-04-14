using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField] GameObject impactParticles;
    AttackableObject target;
    float damage = 0;
   [SerializeField] float speed = 5f;

    public BasicProjectile(AttackableObject attackableObject, float Damage)
    {
        target = attackableObject;
        damage = Damage;
    }

    public void SetTarget(AttackableObject attackableObject)
    {
        target = attackableObject;
        Debug.Log(attackableObject);
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

            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out AttackableObject attackableObject))
        {
            if (attackableObject == target)
            {
                Instantiate(impactParticles, transform.position, transform.rotation);
                Attack();
            }
        }
    }

    protected virtual void Attack()
    {
        target.ReceiveDamage(damage);

        Destroy(this);
    }
}
