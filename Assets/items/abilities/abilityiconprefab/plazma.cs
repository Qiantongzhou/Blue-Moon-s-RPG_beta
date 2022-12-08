using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plazma : EnhancementSkill
{
    // Start is called before the first frame update
    public override string Name => "";

    public override string Description => "";

    public override void DoAction()
    {
        GameObject.FindWithTag("Player").GetComponent<character>().vfxnumber = 3;

    }
}
