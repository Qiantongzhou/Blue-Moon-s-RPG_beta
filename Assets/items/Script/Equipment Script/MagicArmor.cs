using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicArmor : EffectEquipment
{
    [SerializeField]
    private int convertedHealth;



    public override Attributes attributeList => GetComponent<Attributes>();

    public override string Name => "Magic Armor";

    public override string Description => "";

    public override kind GetKinds()
    {
        return kind.normal;
    }



   

    // Update is called once per frame
    void LateUpdate()
    {
        if (equipped)
        {
            if (!added)
            {
                convertedHealth = (int)Mathf.Floor(player.ResultAttr.healthpoint * 0.5f);


                player.equipAttr.attackdamagebonus += convertedHealth;
                added = true;
            }
        }
    }

    protected override void OnRemove()
    {

        player.equipAttr.attackdamagebonus -= convertedHealth;
    }
}
