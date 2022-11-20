using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillInventory : MonoBehaviour, IDropHandler
{
    EmptySkill empty;
    private void Awake()
    {
        empty = gameObject.AddComponent<EmptySkill>();

        if (transform.childCount==0)
        {
            GetComponentInParent<SlotManager>().attachSkill(int.Parse(name.Substring(11, 1)) - 1, empty);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            eventData.pointerDrag.GetComponent<inventory>().parentimg = transform;
            GetComponentInParent<SlotManager>().attachSkill(int.Parse(name.Substring(11, 1)) - 1, eventData.pointerDrag.GetComponent<Skills>());
        }
    }

    private void OnTransformChildrenChanged()
    {
        if(transform.childCount == 0)
        {
            GetComponentInParent<SlotManager>().attachSkill(int.Parse(name.Substring(11, 1)) - 1, empty);
        }
    }
}
