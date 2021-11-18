using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField]
    List<Slot> EquipmentSlot_List = new List<Slot>();

    // Start is called before the first frame update
    void Start()
    {
        InitEquipmentSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitEquipmentSlot()
    {
        for(int i = 0; i < EquipmentSlot_List.Count; i++)
        {
            EquipmentSlot_List[i].m_Equiptment = this.gameObject.GetComponent<EquipmentUI>();
        }
    }

    public void SetEquipementSlot()
    {
        for(int i = 0; i < EquipmentSlot_List.Count; i++)
        {
            // 현재 선택한 슬롯의 아이템의 타입이 EquipmentSlot_List[i] 의 타입과 동일하다면
            // i번째 슬롯의 이미지와 아이템을 현재 선택한 슬롯의 아이템 이미지로 변경하고
            // i번째 슬롯의 isEquiptment 를 true 로 변경한다.
            // isEquipment 가 true 일때 item 의 status 를 player 의 status 에 더해준다. 
        }
    }
}
