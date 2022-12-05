using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Skills;

public abstract class Equipment : MonoBehaviour
{
    public enum kind { empty, normal};

    abstract public Attributes attributeList { get; }
    abstract public string Name { get; }
    abstract public string Description { get; }

    public abstract kind GetKinds();

}
