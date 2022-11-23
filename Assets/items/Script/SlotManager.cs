using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    
    SkillManager skillManager;

    private void Awake()
    {
        skillManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillManager>();
    }


    public void attachSkill(int slot, Skills skill)
    {
        skillManager.skills[slot] = skill;
        if (skill.GetKinds() != Skills.kinds.empty)
        {
            skillManager.updateSkill();
        }
    }

}
