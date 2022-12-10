using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rustedarmer : Equipment
{
    public override Attributes attributeList => transform.GetComponent<Attributes>();

    public override string Name =>"rustedarmor";

    public override string Description => "not good";

    public override kind GetKinds()
    {
        return kind.normal;
    }

    // Start is called before the first frame update
  
}
