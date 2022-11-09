using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skills : MonoBehaviour
{
    private int slots;

    public enum kinds {attribute, boost, skillEnhancement, empty};

    abstract public string Name { get; }
    abstract public string Description { get; }
    public abstract kinds GetKinds();
    public abstract int GetSlots();
}
