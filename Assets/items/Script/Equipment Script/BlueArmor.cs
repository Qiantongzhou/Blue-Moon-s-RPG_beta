using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueArmor : Equipment
{
    public override Attributes attributeList => GetComponent<Attributes>();

    public override string Name => "Blue Armor";

    public override string Description => "some description";

    public override kind GetKinds()
    {
        return kind.normal;
    }
}
