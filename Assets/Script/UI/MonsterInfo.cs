using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterInfo : MonoBehaviour
{
    public TextMeshProUGUI m_Monster_Name;
    public GameObject m_Player_Obj;
    public GameObject m_UI_Obj;
    public RectTransform m_RectBackground;  // UI바의 배경
    public RectTransform m_RectBar;  // UI바

    [SerializeField]
    bool isHP;
    [SerializeField]
    bool isMP;
    [SerializeField]
    bool isExp;

    void Update()
    {
        PlayerCharacter Player = m_Player_Obj.GetComponent<PlayerCharacter>();
        m_UI_Obj = Player.GetAttackMonster();

        if (m_UI_Obj.GetComponent<Status>() && m_RectBackground && m_RectBar && m_Monster_Name)
        {
            if (isHP)
                SetHP_UI();
            else if (isMP)
                SetMP_UI();
            else if (isExp)
                SetEXP_UI();

            m_Monster_Name.text = m_UI_Obj.GetComponent<Status>().GetName();

        }
    }

    public void SetHP_UI()
    {
        Status Status_Obj = m_UI_Obj.GetComponent<Status>();
        float fRat = Status_Obj.GetStatus("Cur_HP") / Status_Obj.GetOriginalStatus("HP");  // 현재 체력에서 최대 체력을 나누어 비율 구하기
        Vector2 vBGSize = m_RectBackground.sizeDelta;  // 각 UI바의 길이 구하기
        Vector2 vBarSize = m_RectBar.sizeDelta;
        vBarSize.x = vBGSize.x * fRat;  // 변경할 UI바의 길이.x의 값을 전체 길이에서 비율만큼 곱한 크기로 바꾸기
        m_RectBar.sizeDelta = vBarSize;  // 변경할 UI바 길이 갱신하기
    }

    public void SetMP_UI()
    {
        Status Status_Obj = m_UI_Obj.GetComponent<Status>();
        float fRat = Status_Obj.GetStatus("Cur_MP") / Status_Obj.GetOriginalStatus("MP");  // 현재 체력에서 최대 체력을 나누어 비율 구하기
        Vector2 vBGSize = m_RectBackground.sizeDelta;  // 각 UI바의 길이 구하기
        Vector2 vBarSize = m_RectBar.sizeDelta;
        vBarSize.x = vBGSize.x * fRat;  // 변경할 UI바의 길이.x의 값을 전체 길이에서 비율만큼 곱한 크기로 바꾸기
        m_RectBar.sizeDelta = vBarSize;  // 변경할 UI바 길이 갱신하기
    }

    public void SetEXP_UI()
    {
        Status Status_Obj = m_UI_Obj.GetComponent<Status>();
        float fRat = Status_Obj.GetStatus("EXP") / Status_Obj.GetOriginalStatus("Max_EXP");  // 현재 체력에서 최대 체력을 나누어 비율 구하기
        Vector2 vBGSize = m_RectBackground.sizeDelta;  // 각 UI바의 길이 구하기
        Vector2 vBarSize = m_RectBar.sizeDelta;
        vBarSize.x = vBGSize.x * fRat;  // 변경할 UI바의 길이.x의 값을 전체 길이에서 비율만큼 곱한 크기로 바꾸기
        m_RectBar.sizeDelta = vBarSize;  // 변경할 UI바 길이 갱신하기
    }
}
