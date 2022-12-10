using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titainarmor : Equipment
{
    public override Attributes attributeList => transform.GetComponent<Attributes>();

    public override string Name => "strong armor";

    public override string Description =>"god";

    public override kind GetKinds()
    {
        return kind.normal;
    }
}
