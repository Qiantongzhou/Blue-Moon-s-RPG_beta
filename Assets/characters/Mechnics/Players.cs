using System;
using System.Collections.Generic;
using UnityEngine;
public static class Players
{
    public static List<GameObject> DamageReceivers = new List<GameObject>();
    public static event EventHandler<GameObject> OnPlayerChange;

    public static void AddDamageReceiver(GameObject damageReceiver)
    {
        DamageReceivers.Add(damageReceiver);
        OnPlayerChange?.Invoke(null, damageReceiver);
    }
}

