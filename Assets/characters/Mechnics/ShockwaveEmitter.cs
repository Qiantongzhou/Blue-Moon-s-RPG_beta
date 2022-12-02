using UnityEngine;
public class ShockwaveEmitter : MonoBehaviour
{
    [SerializeField]
    private GameObject ShockwavePrefab;

    private void InstantiateShockwave()
    {
        Instantiate(ShockwavePrefab, transform.position, transform.rotation);
    }
        
}
