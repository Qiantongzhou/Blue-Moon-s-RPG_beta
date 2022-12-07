using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    public PaladinController paladin;
    public character character;
    public Transform[] spawnPosition;
    public GameObject[] snowBallPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy(gameObject, 10.0f);
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("SnowBALL");
            paladin.transform.rotation = Quaternion.Euler(new Vector3(0, paladin.transform.rotation.eulerAngles.y, 0));
            character.transform.rotation = Quaternion.Euler(new Vector3(0, character.transform.rotation.eulerAngles.y, 0));
            Vector3 forceDir = new Vector3(0.0f, 1.5f, 0.0f);
            //paladin.rb.velocity = Vector3.zero;
            paladin.rb.AddRelativeForce(forceDir, ForceMode.Impulse);
            character.rb.AddRelativeForce(forceDir, ForceMode.Impulse);
            Destroy(this.gameObject);
        }
    }

    IEnumerator waitSpawn()
    {
        int randomTime = Random.Range(6, 10);
        yield return new WaitForSeconds(randomTime);
        int randomSpawPoint = Random.Range(0, spawnPosition.Length);
        Instantiate(snowBallPrefabs[0], spawnPosition[randomSpawPoint].position, transform.rotation);
        StartCoroutine(waitSpawn());
    }
}
