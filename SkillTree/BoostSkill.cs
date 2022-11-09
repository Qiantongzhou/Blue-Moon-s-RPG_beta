using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoostSkill : Skills
{
    private const kinds bst = kinds.boost;
    //the boosts are the passive skills

    public enum triggerType {continuesly, onJump, onHit, onDamage, onKill}


    public override kinds GetKinds()
    {
        return bst;
    }
    public abstract triggerType GetTriggerType();

}
