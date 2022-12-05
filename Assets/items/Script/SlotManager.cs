using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    
    SkillManager skillManager;
    EquipmentManager equipmentManager;

    private void Awake()
    {
        skillManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillManager>();
        equipmentManager = GameObject.FindGameObjectWithTag("Player").GetComponent<EquipmentManager>();
    }


    public void attachSkill(int slot, Skills skill)
    {
        skill.slots = slot;
        skillManager.skills[slot] = skill;
        skillManager.updateSkill();

    }

    public void attachEquipment(int slot, Equipment equipment)
    {
        equipmentManager.equipments[slot] = equipment;
        equipmentManager.updateEquipment();
    }

}
