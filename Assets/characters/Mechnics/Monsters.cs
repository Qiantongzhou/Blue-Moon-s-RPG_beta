using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
