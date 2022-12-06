using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Attributes TempAttr;
    public Equipment[] equipments = new Equipment[8];
    private player player;





    // Start is called before the first frame update
    void Update()
    {
        player = GetComponent<player>();
        TempAttr = player.equipAttr;
    }

    public void updateEquipment()
    {

        bool attrChanged = false;
        TempAttr.SetZero();
        for (int i = 0; i < equipments.Length; i++)
        {
            if (equipments[i] != null)
            {
                if (equipments[i].GetKinds() == Equipment.kind.normal)
                {
                    TempAttr += (equipments[i]).attributeList;
                    if (!attrChanged)
                    {
                        attrChanged = true;
                    }
                }

            }
        }
        if (attrChanged)
        {
            player.SetEquipAttributes(TempAttr);
        }
    }

}
