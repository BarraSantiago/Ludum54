using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget 
{
    public void ReceiveDamage(float damage);
    public bool isValid();
    public Team GetTeam();
    public Transform GetTransform();
}
