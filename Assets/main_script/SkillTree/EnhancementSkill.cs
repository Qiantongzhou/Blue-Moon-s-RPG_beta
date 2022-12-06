using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnhancementSkill : Skills
{
    private const kinds ehs = kinds.skillEnhancement;


    public override kinds GetKinds()
    {
        return ehs;
    }

    public KeyCode GetkeyBind()
    {
        switch (GetSlots())
        {
            case 0: return KeyCode.Alpha1;
            case 1: return KeyCode.Alpha2;
            case 2: return KeyCode.Alpha3;
            case 3: return KeyCode.Alpha4;
            case 4: return KeyCode.Alpha5;
            case 5: return KeyCode.Alpha6;
            case 6: return KeyCode.Alpha7;
            case 7: return KeyCode.Alpha8;
            default: return KeyCode.None;
        }
    }



    public abstract void DoAction();
}
