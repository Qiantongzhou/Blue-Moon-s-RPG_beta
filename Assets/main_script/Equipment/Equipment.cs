using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Skills;

public abstract class Equipment : MonoBehaviour
{
    protected bool equipped=false;
    public enum kind { empty, normal, effect};

    abstract public Attributes attributeList { get; }
    abstract public string Name { get; }
    abstract public string Description { get; }

    public abstract kind GetKinds();

}
