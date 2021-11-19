using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item m_item;
    public int m_itemCount;
    public Image m_itemImage;

    public enum SlotType
    {
        Inventory,
        Equipment,
    };

    public enum Equiptment
    {
        Weapon,
        Head,
        Body,
        Pants,
        Boots,
        Notting,
    };

    public bool isEquipment = false;  // 슬롯 내 장착한 장비 여부 판단
    public SlotType m_Slot_Type;
    public Equiptment m_Equiptment_Type;
    public Inventory m_Inventory;
    public EquipmentUI m_Equiptment;
    public Status m_Obj_Status;

    public void UpdatePlayerStatus()  // 버튼에 추가할 것
    {
        GameManager.Getinstance().m_Player.GetComponent<Status>().SetItemStatus(m_Obj_Status);
    }

    public void SetData(Item item, Image itemimg, bool isequip, SlotType slottype, Equiptment equiptype)
    {
        this.m_item = item;
        this.m_itemImage.sprite = itemimg.sprite;
        this.isEquipment = isequip;
        this.m_Slot_Type = slottype;
        this.m_Equiptment_Type = equiptype;
    }

    public void SetColor(float alpha)
    {
        Color color = m_itemImage.color;
        color.a = alpha;
        m_itemImage.color = color;
    }

    public void OnItemInfo()
    {
        if (m_item != null)
        {
            Slot TouchSlot = this.gameObject.GetComponent<Slot>();
            ItemInfoUI InfoUI = m_Inventory.ReturnInfoObj();
            m_Inventory.SetItemInfo(TouchSlot);
            InfoUI.SetItemInfo(m_item.m_Equipment_Comment);
        }
    }

    public void StetSlotItem(Item item)
    {
        m_item = item;
    }

    public void SetItemImage(Sprite image)
    {
        m_itemImage.sprite = image;
        SetColor(1);
    }

    public void AddItem(Item item, Status status, int count = 1)
    {
        m_item = item;
        m_itemCount = count;
        m_itemImage.sprite = item.m_Equipment_Image;
        m_Equiptment_Type = (Equiptment)(int)item.m_Equipment_Type;
        m_Obj_Status.StatusCopy(status);
        SetColor(1);
    }

    public void SetSelectSlot()
    {
        Slot slot = this;
        UIManager.Getinstance().SelectSlot(ref slot, m_Obj_Status);
    }

    public void SetSlotStatus(Status status)
    {
        m_Obj_Status.StatusCopy(status);
    }

    public void CopySlot(Slot slot)
    {
        m_item = slot.m_item;
        Debug.Log($"Slot Item : {slot.m_item}");
        m_itemCount = slot.m_itemCount;
        m_itemImage.sprite = slot.m_itemImage.sprite;
        m_Equiptment_Type = (Equiptment)(int)slot.m_Equiptment_Type;
        m_Obj_Status.StatusCopy(slot.m_Obj_Status);
    }

    public void ClearSlot()
    {
        m_item = null;
        m_itemCount = 0;
        m_itemImage.sprite = null;
        isEquipment = false;
        SetColor(0);
    }

}
