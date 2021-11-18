using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]  // 에셋 메뉴를 생성한다.
public class Item : ScriptableObject // 스크립트 자체를 오브젝트로 만든다.
{
    public enum EquipmentType
    {
        Weapon,
        Head,
        Body,
        Pants,
        Boots,
        Notting,
    };

    public string m_Equipment_Name;  // 장비 이름
    public string m_Equipment_Comment; // 장비 설명
    public EquipmentType m_Equipment_Type;  // 장비 타입
    public Sprite m_Equipment_Image;  // 장비 이미지(sprite)
    public GameObject m_Equipment_Prefab; // 장비 프리팹
    public bool isEquipment = false;  // 장비의 능력치를 주기 위해 판단
}
