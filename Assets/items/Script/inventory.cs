using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class inventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    public Transform parentimg;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Time.timeScale = 0.0f;
        parentimg = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        transform.GetChild(0).GetComponent<Image>().raycastTarget = false;
        transform.GetChild(1).GetComponent<Image>().raycastTarget = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentimg);
        transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
        transform.GetChild(1).GetComponent<Image>().raycastTarget = true;
        Time.timeScale = 1.0f;
    }
}
