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
    bool died = false;

    float timer;
    private void Awake()
    {
        gamestatistics.housecount += 1;
    }
    private void Start()
    {
        
        health = (int)(health * DamageCalculator.multiPerEnemy);
        currenthealth = health;
        animator = transform.GetComponent<Animator>();
        source = transform.AddComponent<AudioSource>();
        source.clip= destory;

    }
    private void Update()
    {
       
            
          
       
      
        
    }
    public void takedamage(int amount)
    {
        gamestatistics.maxhealth = health;
        gamestatistics.currenthealth = currenthealth;

        gamestatistics.isunderattack = true;
        currenthealth -= amount;
        if (currenthealth < 0&&!died)
        {
            died = true;
            currenthealth = 0;
            gamestatistics.housecount -= 1;
            destorytraget();
        }
    }

    public void destorytraget()
    {
        if (animator != null)
        {
            animator.SetBool("destory", true);
        }
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
