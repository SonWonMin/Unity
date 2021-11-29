using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField]
    List<Slot> EquipmentSlot_List = new List<Slot>();
    [SerializeField]
    GameObject m_Equipment_Obj;
    [SerializeField]
    GameObject m_Equipment_Info_Obj;
    [SerializeField]
    Slot m_Item_Info_Slot;

    // Start is called before the first frame update
    void Start()
    {
        InitEquipmentSlot();
    }

    public void InitEquipmentSlot()
    {
        for(int i = 0; i < EquipmentSlot_List.Count; i++)
        {
            EquipmentSlot_List[i].m_Equiptment = this.gameObject.GetComponent<EquipmentUI>();
        }
    }

    public ItemInfoUI ReturnInfoObj()
    {
        return m_Equipment_Info_Obj.GetComponent<ItemInfoUI>();
    }

    public void OffEquipmentInfo()
    {
        if (m_Equipment_Info_Obj.activeSelf)
        {
            m_Equipment_Obj.transform.localPosition = new Vector3(0, 0, 0);
            m_Equipment_Info_Obj.SetActive(false);
        }
    }

    public void UnEquipment()
    {
        UIManager UI = UIManager.Getinstance();
        PlayerCharacter player = GameManager.Getinstance().m_Player.GetComponent<PlayerCharacter>();
        Status PlayerStat = player.GetComponent<Status>();

        PlayerStat.SetItemStatus(UI.ReturnSelectStatus(), "-");  // 능력치를 뺀다.
        player.ReturnPlayerInventory().AcquireItem(UI.ReturnSelectSlot());
        UI.ReturnSelectSlot().ClearSlot();
    }

    public void SetItemInfo(Slot touchslot)
    {
        m_Equipment_Obj.transform.localPosition = new Vector3(-350, 0, 0);
        m_Item_Info_Slot.SetData(touchslot.m_item, touchslot.m_itemImage, touchslot.isEquipment, touchslot.m_Slot_Type, touchslot.m_Equiptment_Type);
        m_Item_Info_Slot.m_Obj_Status.StatusCopy(touchslot.m_Obj_Status);
        m_Equipment_Info_Obj.SetActive(true);
    }

    public void SetEquipementSlot()
    {
        UIManager UI = UIManager.Getinstance();
        for(int i = 0; i < EquipmentSlot_List.Count; i++)
        {
            if(EquipmentSlot_List[i].m_Equiptment_Type == UI.ReturnSelectSlot().m_Equiptment_Type)
            {
                PlayerCharacter player = GameManager.Getinstance().m_Player.GetComponent<PlayerCharacter>();
                Status PlayerStat = player.GetComponent<Status>();
                Status SlotStat = EquipmentSlot_List[i].GetComponent<Status>();

                if (EquipmentSlot_List[i].isEquipment == false)
                {
                    EquipmentSlot_List[i].SetItemImage(UI.ReturnSelectSlot().m_itemImage.sprite);
                    EquipmentSlot_List[i].SetSlotStatus(UI.ReturnSelectSlot().m_Obj_Status);
                    EquipmentSlot_List[i].StetSlotItem(UI.ReturnSelectSlot().m_item);
                    EquipmentSlot_List[i].isEquipment = true;
                    UI.ReturnSelectSlot().ClearSlot();
                }
                else 
                {
                    PlayerStat.SetItemStatus(SlotStat, "-");  // 능력치를 뺀다.
                    UIManager.Getinstance().m_temp_Slot.CopySlot(UI.ReturnSelectSlot());
                    UI.ReturnSelectSlot().ClearSlot();
                    player.ReturnPlayerInventory().AcquireItem(EquipmentSlot_List[i]);
                    EquipmentSlot_List[i].CopySlot(UIManager.Getinstance().m_temp_Slot);
                }
                PlayerStat.SetItemStatus(SlotStat);
            }
            // 현재 선택한 슬롯의 아이템의 타입이 EquipmentSlot_List[i] 의 타입과 동일하다면
            // i번째 슬롯의 이미지와 아이템을 현재 선택한 슬롯의 아이템 이미지로 변경하고
            // i번째 슬롯의 isEquiptment 를 true 로 변경한다.
            // isEquipment 가 true 일때 item 의 status 를 player 의 status 에 더해준다. 
        }
    }
}
