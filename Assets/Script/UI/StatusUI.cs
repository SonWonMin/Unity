using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
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
        if (m_UI_Obj && m_RectBackground && m_RectBar)
        {
            if (isHP)
                SetHP_UI();
            else if (isMP)
                SetMP_UI();
            else if (isExp)
                SetEXP_UI();

            if (m_RectBar.sizeDelta.x > m_RectBackground.sizeDelta.x)
            {
                m_RectBar.sizeDelta = m_RectBackground.sizeDelta;
            }
        }
    }

    public void SetHP_UI()
    {
        Status Status_Obj = m_UI_Obj.GetComponent<Status>();
        if (Status_Obj.GetStatus("Max_HP") > Status_Obj.GetStatus("Cur_HP"))
        { 
            float fRat = Status_Obj.GetStatus("Cur_HP") / Status_Obj.GetOriginalStatus("HP");  // 현재 체력에서 최대 체력을 나누어 비율 구하기
            Vector2 vBGSize = m_RectBackground.sizeDelta;  // 각 UI바의 길이 구하기
            Vector2 vBarSize = m_RectBar.sizeDelta;
            vBarSize.x = vBGSize.x * fRat;  // 변경할 UI바의 길이.x의 값을 전체 길이에서 비율만큼 곱한 크기로 바꾸기
            m_RectBar.sizeDelta = vBarSize;  // 변경할 UI바 길이 갱신하기
            //LeanTween.size(m_RectBar, vBarSize, 0.07f);
        }
    }

    public void SetMP_UI()
    {
        Status Status_Obj = m_UI_Obj.GetComponent<Status>();
        if (Status_Obj.GetStatus("Max_MP") > Status_Obj.GetStatus("Cur_MP"))
        {
            float fRat = Status_Obj.GetStatus("Cur_MP") / Status_Obj.GetOriginalStatus("MP");  // 현재 체력에서 최대 체력을 나누어 비율 구하기
            Vector2 vBGSize = m_RectBackground.sizeDelta;  // 각 UI바의 길이 구하기
            Vector2 vBarSize = m_RectBar.sizeDelta;
            vBarSize.x = vBGSize.x * fRat;  // 변경할 UI바의 길이.x의 값을 전체 길이에서 비율만큼 곱한 크기로 바꾸기
            m_RectBar.sizeDelta = vBarSize;  // 변경할 UI바 길이 갱신하기
            //LeanTween.size(m_RectBar, vBarSize, 0.07f);
        }
    }

    public void SetEXP_UI()
    {
         Status Status_Obj = m_UI_Obj.GetComponent<Status>();
        if (Status_Obj.GetStatus("Max_EXP") > Status_Obj.GetStatus("EXP"))
        {
            float fRat = Status_Obj.GetStatus("EXP") / Status_Obj.GetOriginalStatus("Max_EXP");  // 현재 체력에서 최대 체력을 나누어 비율 구하기
            Vector2 vBGSize = m_RectBackground.sizeDelta;  // 각 UI바의 길이 구하기
            Vector2 vBarSize = m_RectBar.sizeDelta;
            vBarSize.x = vBGSize.x * fRat;  // 변경할 UI바의 길이.x의 값을 전체 길이에서 비율만큼 곱한 크기로 바꾸기
            m_RectBar.sizeDelta = vBarSize;  // 변경할 UI바 길이 갱신하기
            //LeanTween.size(m_RectBar, vBarSize, 0.07f);
        }
    }

}
