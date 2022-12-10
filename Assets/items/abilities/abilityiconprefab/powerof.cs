using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerof : EnhancementSkill
{
    public override string Name => "";

    public override string Description => "";

    public override void DoAction()
    {
        GameObject.FindWithTag("Player").GetComponent<character>().vfxnumber = 9;
    }

}
