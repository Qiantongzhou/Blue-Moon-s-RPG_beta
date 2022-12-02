using UnityEngine;

public class NPC_POS : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] NPC;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DamageCalculator.eliteCount > 0)
        {
            if (Random.Range(0, 100) == 90)
            {
                Instantiate(NPC[0], transform.position, Quaternion.identity);
            }
        }
        if (DamageCalculator.currentcreatureCount < DamageCalculator.maxcreatureCount)
        {
            if (Random.Range(0, 100) == 90)
            {
                Instantiate(NPC[1], transform.position, Quaternion.identity);
                DamageCalculator.currentcreatureCount++;
            }
        }

    }
}
