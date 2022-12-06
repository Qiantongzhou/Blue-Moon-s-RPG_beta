using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class defensetarget : MonoBehaviour
{
   public int health;
    public int currenthealth;
   public AudioClip destory;
    AudioSource source;
    Animator animator;
    bool takedamaged;
    float timer;
    private void Start()
    {
        gamestatistics.housecount += 1;
        health = (int)(health * DamageCalculator.multiPerEnemy);
        currenthealth = health;
        animator = transform.GetComponent<Animator>();
        source = transform.AddComponent<AudioSource>();
        source.clip= destory;

    }
    private void Update()
    {
        if (takedamaged)
        {
            gamestatistics.isunderattack = true;
            timer = 5.0f;
        }
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            takedamaged = false;
            gamestatistics.isunderattack = false;
        }
        
    }
    public void takedamage(int amount)
    {
        gamestatistics.maxhealth = health;
        gamestatistics.currenthealth = currenthealth;

        takedamaged = true;
        currenthealth -= amount;
        if (currenthealth < 0)
        {
            currenthealth = 0;
            gamestatistics.housecount -= 1;
            destorytraget();
        }
    }

    public void destorytraget()
    {
        animator.SetBool("destory", true);
        source.Play();
        StartCoroutine(setfakse());
        Destroy(gameObject,4f);
    }

    IEnumerator setfakse()
    {
        yield return new WaitForSeconds(3f);
        gamestatistics.isunderattack = false;
    }

}
