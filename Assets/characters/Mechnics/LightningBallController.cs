using UnityEngine;
public class LightningBallController : MonoBehaviour
{
    [SerializeField]
    private float LifeTime = 10;

    private float deathTime;
    private bool launched = false; 
    
    public void Launch()
    {
        launched = true;
        deathTime = LifeTime + Time.time;
    }
    private void Update()
    {
        if (launched)
        {
            if (deathTime <= Time.time)
            {
                Destroy(gameObject);
            }
        }
    }
}
