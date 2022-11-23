using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Skills[] skills = new Skills[8];
    private EnhancementSkill[] activeSkills = new EnhancementSkill[8];
    private int skillAmount=0;

    private void Awake()
    {
        /*for(int i = 0; i < skills.Length; i++)
        {
            if (skills[0].GetKinds() == Skills.kinds.skillEnhancement)
            {
                activeSkills[i] = (EnhancementSkill) skills[i];
                skillAmount++;
            }
        }*/

    }

    private void Start()
    {
        /*foreach(Skills skill in skills)
        {
            if (skill.GetKinds() == Skills.kinds.attribute)
                {
                    *//*foreach(Attribute in skills[0].attributeList)
                        {
                             the Action to add attributes to player
                        }*//*
                }
        }*/

    }

    private void Update()
    {
        if (activeSkills[0] != null)
        {
            if (Input.GetKeyDown(activeSkills[0].GetkeyBind()))
            {
                print("skill 1 active");
                activeSkills[0].DoAction();
            }
        }
        if (activeSkills[1] != null)
        {
            if (Input.GetKeyDown(activeSkills[1].GetkeyBind()))
            {
                
                activeSkills[1].DoAction();
            }
        }
        if (activeSkills[2] != null)
        {
            if (Input.GetKeyDown(activeSkills[2].GetkeyBind()))
            {
                activeSkills[2].DoAction();
            }
        }
        if (activeSkills[3] != null)
        {
            if (Input.GetKeyDown(activeSkills[3].GetkeyBind()))
            {
                activeSkills[3].DoAction();
            }
        }
        if (activeSkills[4] != null)
        {
            if (Input.GetKeyDown(activeSkills[4].GetkeyBind()))
            {
                activeSkills[4].DoAction();
            }
        }
        if (activeSkills[5] != null)
        {
            if (Input.GetKeyDown(activeSkills[5].GetkeyBind()))
            {
                activeSkills[5].DoAction();
            }
        }
        if (activeSkills[6] != null)
        {
            if (Input.GetKeyDown(activeSkills[6].GetkeyBind()))
            {
                activeSkills[6].DoAction();
            }
        }
        if (activeSkills[7] != null)
        {
            if (Input.GetKeyDown(activeSkills[7].GetkeyBind()))
            {
                activeSkills[7].DoAction();
            }
        }
    }

    public void updateSkill()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].GetKinds() == Skills.kinds.skillEnhancement)
            {
                activeSkills[i] = (EnhancementSkill)skills[i];
                skillAmount++;
            }
        }
    }
}
