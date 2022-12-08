using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sleepbow :Equipment
{
    public override Attributes attributeList => transform.GetComponent<Attributes>();

    public override string Name => "";

    public override string Description => "";

    public override kind GetKinds()
    {
        throw new System.NotImplementedException();
    }


}
