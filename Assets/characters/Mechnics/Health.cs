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

    public void TakeDamage(float damageAmount, Vector3 direction)
    {
        Debug.Log("TakeDamage " + damageAmount +" Health Remain " + CurrentHealthPoint);
        CurrentHealthPoint -= damageAmount;
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
        myAnimator.SetTrigger(Dead);
        IsAlive = false;
    }
}
