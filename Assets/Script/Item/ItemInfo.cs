using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public Item item;
    Status m_Item_Status;

    private void Start()
    {
        m_Item_Status = this.gameObject.GetComponent<Status>();
    }

    public Item ReturnItem()
    {
        return item;
    }
}
