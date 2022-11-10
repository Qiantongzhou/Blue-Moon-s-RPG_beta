using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySkill : Skills
{
    public int slot;

    public EmptySkill(int slot)
    {
        this.slot = slot;
    }

    public override string Name => "Empty skill slot";

    public override string Description => "An empty skill slot is ready for equip";

    public override kinds GetKinds()
    {
        return kinds.empty;
    }

    public override int GetSlots()
    {
        return slot;
    }

}
