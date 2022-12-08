using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler<Vector3> OnHurt;
    public event EventHandler OnDead;
    [SerializeField]
    private float MaxHealthPoint, SpawnHealthPoint;
    public bool IsAlive;
    private float CurrentHealthPoint;

    private void Awake()
    {
        CurrentHealthPoint = SpawnHealthPoint;
        IsAlive = true;
    }

    public void HealthChange(float damageAmount, Vector3 direction)
    {
        if (IsAlive)
        {
            CurrentHealthPoint += damageAmount;

            OnHurt?.Invoke(gameObject, direction);
            if (CurrentHealthPoint <= 0)
            { Die(); }
        }
    }
    private void Die()
    {
        IsAlive = false;
        OnDead?.Invoke(gameObject, null);
    }
    public float HealthPoint()
    {
        return CurrentHealthPoint;
    }
}
