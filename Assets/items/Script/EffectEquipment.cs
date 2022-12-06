using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectEquipment : Equipment
{
    protected player player;
    protected bool added = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    }
    private void OnTransformParentChanged()
    {
        if (!transform.parent.name.Contains("EquipSlot"))
        {
            equipped = false;
            if (added)
            {
                OnRemove();
                added = false;
            }
            return;
        }
        if (transform.parent.name.Contains("EquipSlot"))
        {
            equipped = true;
            return;
        }
    }

    public void reloading()
    {
        added = false;
    }

    protected abstract void OnRemove();
}
