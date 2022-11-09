using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    private Collider[] WeaponColliders;

    private void WeaponBegin(int index)
    {
        WeaponColliders[index].enabled = true;
    }
    private void WeaponEnd(int index)
    {
        WeaponColliders[index].enabled = false;
    }
}
