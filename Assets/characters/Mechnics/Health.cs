using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler<Vector3> OnHurt;
    public event EventHandler OnDead;
    [SerializeField]
    private float MaxHealthPoint, SpawnHealthPoint;
    public bool IsAlive;

    private readonly string Hurt = "Hurt", Dead = "Dead";

    private float CurrentHealthPoint;
    private Animator myAnimator;

    private void Awake()
    {
        CurrentHealthPoint = SpawnHealthPoint;
        IsAlive = true;
        myAnimator = GetComponent<Animator>();
    }

    public void HealthChange(float damageAmount, Vector3 direction)
    {
        CurrentHealthPoint += damageAmount;

        myAnimator.SetTrigger(Hurt);
        OnHurt?.Invoke(gameObject, direction);
        if (CurrentHealthPoint <= 0)
        {
            Die();
            return;
        }

    }
    private void Die()
    {
        myAnimator.SetFloat("Movement", 0);
        myAnimator.SetTrigger(Dead);
        IsAlive = false;
        OnDead?.Invoke(gameObject, null);
    }
    public float HealthPoint()
    {
        return CurrentHealthPoint;
    }
}
