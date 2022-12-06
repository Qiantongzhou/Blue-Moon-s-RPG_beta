using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class MagesHand : EffectEquipment
{
    public override Attributes attributeList => GetComponent<Attributes>();

    public override string Name => "Mage\'s Hand";

    public override string Description => "";

    public override kind GetKinds()
    {
        return kind.normal;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (equipped)
        {
            if (!added)
            {
                if ((player.getcurrentmagic() / player.ResultAttr.magicpoint) > 80)
                {
                    player.equipAttr.critchance += 50;
                    added = true;
                }
            }
            else
            {
                if ((player.getcurrentmagic() / player.ResultAttr.magicpoint) < 80)
                {
                    player.equipAttr.critchance -= 50;
                    added = false;
                }
            }
        }
    }

    protected override void OnRemove()
    {
        if (added)
        {
            player.equipAttr.critchance -= 50;
            added = false;
        }
    }
}
