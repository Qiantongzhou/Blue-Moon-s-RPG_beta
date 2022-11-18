using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler<Vector3> OnHurt;
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
        Debug.Log("HealthChange " + damageAmount +" Health Remain " + CurrentHealthPoint);
        CurrentHealthPoint += damageAmount;
        myAnimator.SetTrigger(Hurt);
        OnHurt?.Invoke(this.gameObject, direction);
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
    }
    public float HealthPoint()
    {
        return CurrentHealthPoint;
    }
}
