using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttributeSkill : Skills
{
    private const kinds att = kinds.attribute;
    

    abstract public Attributes attributeList {get;}


    override public kinds GetKinds()
    {
        return att;
    }

}
