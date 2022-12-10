using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phenixfur : Equipment
{
    public override Attributes attributeList => transform.GetComponent<Attributes>();

    public override string Name => "fur";

    public override string Description => "god";

    public override kind GetKinds()
    {
       return kind.normal;
    }


}
