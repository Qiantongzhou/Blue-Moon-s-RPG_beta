using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : EnhancementSkill
{
    // Start is called before the first frame update
    public int slot;
    public override string Name => "Zoom";

    public override string Description => "Player forward 2 unit";

    public override void DoAction()
    {
        GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Rigidbody>().AddForce(Vector3.forward*3,ForceMode.Impulse);
    }

    public override KeyCode GetkeyBind()
    {
        return KeyCode.Keypad1;
    }

    public override int GetSlots()
    {
        return slot;
    }
}
