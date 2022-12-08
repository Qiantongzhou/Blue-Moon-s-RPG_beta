using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallSpawn : MonoBehaviour
{
    public Transform[] spawnPosition;
    public GameObject[] snowBallPrefabs;
    void Start()
    {
        StartCoroutine(waitSpawn());
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
