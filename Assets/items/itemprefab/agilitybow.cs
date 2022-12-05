using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agilitybow : EnhancementSkill
{
    public override string Name => "power attack";
    public override string Description => "add attack for five sec";
    public AudioClip clip;
   AudioSource source;
    bool startcount = false;
    float count=0;

    public override void DoAction()
    {
        if (!startcount)
        {
            if (clip != null)
            {
                source = gameObject.AddComponent<AudioSource>();
                source.clip = clip;
                source.volume = 0.2f;
                source.Play();
            }
            print("player attack +20");
            GameObject.FindGameObjectWithTag("Player").GetComponent<player>().attr.attackdamagebonus += 20;
            startcount = true;
        }
    }
    private void Update()
    {
        if (startcount)
        {
            count+=Time.deltaTime;
            if(count > 5)
            {
                print("player attack bounse ennd");
                GameObject.FindGameObjectWithTag("Player").GetComponent<player>().attr.attackdamagebonus -= 20;
                startcount=false;
                count=0;
            }
        }
    }
}
