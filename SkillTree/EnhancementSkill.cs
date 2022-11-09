using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnhancementSkill : Skills
{
    private const kinds ehs = kinds.skillEnhancement;


    public override kinds GetKinds()
    {
        return ehs;
    }

    public abstract KeyCode GetkeyBind();

    public abstract void DoAction();
}
