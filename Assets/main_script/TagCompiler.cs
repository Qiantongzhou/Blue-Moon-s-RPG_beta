using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TagCompiler : MonoBehaviour
{



    private CreatureAttribute[] minionCreatureArribute = ConstEnemyAttributeHolder.creatures;



    public void AddressingTags(GameObject entity, GameTag[] tags)
    {
        if (tags.Contains(GameTag.enemy))
        {
            entity.AddComponent<enemy>();
            if (tags.Contains(GameTag.melee))
            {
                entity.AddComponent<meleattackauto>();
            }
            if (tags.Contains(GameTag.ranged))
            {
                throw new NotImplementedException("ranged enemy script havent implemented 远程敌人脚本未添加");
            }
        }
        else
        {
            if (tags.Contains(GameTag.creature))
            {
                entity.AddComponent<creature>();
            }
        }

        //expandable to all prefebs with GameTag
    }

    public void AddressingAttr(GameObject entity, CreatureTag tags)
    {
        entity.GetComponent<enemy>().InitializeAttr(minionCreatureArribute[(int)tags].range,
                                                    minionCreatureArribute[(int)tags].stopRange,
                                                    minionCreatureArribute[(int)tags].speed,
                                                    minionCreatureArribute[(int)tags].health,
                                                    minionCreatureArribute[(int)tags].attackDamage,
                                                    minionCreatureArribute[(int)tags].healthRegen);
    }
}
