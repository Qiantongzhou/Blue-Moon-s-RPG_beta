using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static Skills;

public class EmptyEquipment : Equipment
{
    public const kind isEmpty = kind.empty;



    public override string Name => "empty";

    public override string Description => "this is the empty equipment slot";

    public override Attributes attributeList => null;

    public override kind GetKinds()
    {
        return isEmpty;
    }


}
