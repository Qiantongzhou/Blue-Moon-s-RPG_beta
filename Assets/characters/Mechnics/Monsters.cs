using System;
using System.Collections.Generic;
using UnityEngine;

public static class Monsters
{
    public static event EventHandler<GameObject> OnAddingMonsterDamageReceiver;
    public static List<GameObject> DamageReceivers = new List<GameObject>();
    public static void AddDamageReceiver(GameObject damageReceiver)
    {
        DamageReceivers.Add(damageReceiver);
        OnAddingMonsterDamageReceiver?.Invoke(null, damageReceiver);
    }
}
