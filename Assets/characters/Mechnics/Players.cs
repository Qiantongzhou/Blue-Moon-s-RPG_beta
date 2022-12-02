using System;
using System.Collections.Generic;
using UnityEngine;

public static class Players
{
    public static event EventHandler<GameObject> OnAddingPlayerDamageReceiver;
    public static List<GameObject> DamageReceivers = new List<GameObject>();
    public static GameObject CurrentPlayer { get; private set; }
    public static void AddDamageReceiver(GameObject damageReceiver)
    {
        DamageReceivers.Add(damageReceiver);
        OnAddingPlayerDamageReceiver?.Invoke(null, damageReceiver);
    }
    public static void SetCurrentPlayer(GameObject gameObject)
    { CurrentPlayer = gameObject; }

}

