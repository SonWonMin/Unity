using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    GameObject m_Inventory_Obj;
    [SerializeField]
    GameObject m_SlotParent_Obj;
    [SerializeField]
    GameObject m_ItemInfo_Obj;

    Slot[] slots;
    [SerializeField]
    Slot m_Item_Info_Slot;

    // Start is called before the first frame update
    void Start()
    {
        slots = m_SlotParent_Obj.GetComponentsInChildren<Slot>();
        InitInventory();
    }

    public ItemInfoUI ReturnInfoObj()
    {
        return m_ItemInfo_Obj.GetComponent<ItemInfoUI>();
    }

    public void InitInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].m_Inventory = this.gameObject.GetComponent<Inventory>();
        }
    }

    public void SetItemInfo(Slot touchslot)
    {
        m_Inventory_Obj.transform.localPosition = new Vector3(-350, 0, 0);
        m_Item_Info_Slot.SetData(touchslot.m_item, touchslot.m_itemImage, touchslot.isEquipment, touchslot.m_Slot_Type, touchslot.m_Equiptment_Type);
        m_Item_Info_Slot.m_Obj_Status.StatusCopy(touchslot.m_Obj_Status);
        m_ItemInfo_Obj.SetActive(true);
    }

    public void OffItemInfo()
    {
        if (m_ItemInfo_Obj.activeSelf)
        {
            m_Inventory_Obj.transform.localPosition = new Vector3(0, 0, 0);
            m_ItemInfo_Obj.SetActive(false);
        }
    }

    public void AcquireItem(Item item, Status status, int count)
    {
        for(int i = 0; i< slots.Length; i++)
        {
            if(slots[i].m_item == null)
            {
                slots[i].AddItem(item, status, count);
                return;
            }
        }    
    }

    public void AcquireItem(Slot slot)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].m_item == null)
            {
                slots[i].AddItem(slot.m_item, slot.m_Obj_Status);
                return;
            }
        }
    }

}
