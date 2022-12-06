using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentInventorys : MonoBehaviour, IDropHandler
{
    EmptyEquipment empty;
    private void Awake()
    {
        empty = gameObject.AddComponent<EmptyEquipment>();

        if (transform.childCount == 0)
        {
            GetComponentInParent<SlotManager>().attachEquipment(int.Parse(name.Substring(11, 1)) - 1, empty);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        
        if (transform.childCount == 0)
        {
            eventData.pointerDrag.GetComponent<inventory>().parentimg = transform;
            GetComponentInParent<SlotManager>().attachEquipment(int.Parse(name.Substring(11, 1)) - 1, eventData.pointerDrag.GetComponent<Equipment>());
        }
    }

    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0)
        {
            GetComponentInParent<SlotManager>().attachEquipment(int.Parse(name.Substring(11, 1)) - 1, empty);
        }
    }
}
