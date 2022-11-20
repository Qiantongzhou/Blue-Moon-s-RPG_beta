using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CreatureAttribute
{
    public CreatureAttribute(float range, float stopRange, float speed, float health, float attackDamage, float healthRegen)
    {
        this.range = range;
        this.stopRange = stopRange;
        this.speed = speed;
        this.health = health;
        this.attackDamage = attackDamage;
        this.healthRegen = healthRegen;
    }

    public float range;
    public float stopRange;
    public float speed;
    public float health;
    public float attackDamage;
    public float healthRegen;
}