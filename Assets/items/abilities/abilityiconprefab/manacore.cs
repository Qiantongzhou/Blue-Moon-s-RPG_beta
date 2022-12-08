using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manacore :EnhancementSkill
{
    public override string Name =>"";

    public override string Description =>"";

    public override void DoAction()
    {
        GameObject.FindWithTag("Player").GetComponent<character>().vfxnumber = 8;
       
    }

    // Start is called before the first frame update

}
