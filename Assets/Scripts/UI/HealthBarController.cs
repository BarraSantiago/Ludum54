using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] Image healthBar;
    AttackableObject attackableObject;

    public void SetAttackableObject(AttackableObject attackable)
    {
        attackableObject = attackable;
    }

    public void OnHealthChange()
    {
        healthBar.fillAmount = (attackableObject.Health / attackableObject.MaxHealth) / 100.0f;
    }


}
