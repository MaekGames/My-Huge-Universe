using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int Health { get; set; }
    void Damage(List<Damage> dmg);
}

[System.Serializable]
public struct Damage
{
    public int amount;
    public DmgType type;
    public Vector3 origin;
    public float damageAmount;
    public float pushForce;
}

public enum DmgType
{
    normal,
    fire
}
