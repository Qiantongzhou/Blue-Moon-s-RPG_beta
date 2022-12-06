using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonArmor : Equipment
{
    public override Attributes attributeList => GetComponent<Attributes>();

    public override string Name => "Dragon Armor";

    public override string Description => "";

    public override kind GetKinds()
    {
        return kind.normal;
    }
}
