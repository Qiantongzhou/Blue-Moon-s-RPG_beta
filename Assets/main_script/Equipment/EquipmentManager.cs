using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Attributes BaseAttr;
    public Attributes TempAttr;
    public Equipment[] equipments = new Equipment[8];
    private player player;





    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<player>();
        BaseAttr = GetComponent<Attributes>();
        TempAttr = gameObject.AddComponent<Attributes>();
    }

    public void updateEquipment()
    {

        bool attrChanged = false;
        player.SetAttributes(BaseAttr);
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
            TempAttr += BaseAttr;
            player.IncreaseAttributes(TempAttr);
            Destroy(TempAttr);
            TempAttr = gameObject.AddComponent<Attributes>();
        }
    }

}
