using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : EnhancementSkill
{
    // Start is called before the first frame update
    public int slot;
    public AudioClip clip;
    public AudioSource source;
    public override string Name => "Zoom";

    public override string Description => "Player forward 2 unit";

    public override void DoAction()
    {if (clip != null)
        {
            source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = 0.05f;
            source.Play();
        }
        GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Rigidbody>().AddForce(GameObject.FindGameObjectWithTag("Player").transform.forward *100,ForceMode.Impulse);
    }
}
