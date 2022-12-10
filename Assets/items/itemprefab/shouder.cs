using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shouder : Equipment
{
    public override Attributes attributeList => transform.GetComponent<Attributes>();

    public override string Name => "shoulder";

    public override string Description => "not good";

    public override kind GetKinds()
    {
        return kind.normal;
    }


}
