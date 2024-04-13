using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget 
{
    public void ReceiveDamage(float damage);
    public Transform GetTransform();
    public Team GetTeam();
}
