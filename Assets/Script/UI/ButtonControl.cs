using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonControl : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField]
    GameObject m_BackObj;
    [SerializeField]
    GameObject m_JoystickObj;
    [SerializeField]
    GameObject m_MoveObj;
    [SerializeField]
    GameObject m_PlayerObj;

    RectTransform m_rectBack;
    RectTransform m_rectJoystick;

    float m_fRadius;  // 반지름
    [SerializeField]
    float m_Range;

    Vector3 m_vecMove;  // 이동 좌표 
    List<Transform> m_TargetTransform = new List<Transform>();
    Transform TargetTF = null;

    bool m_bTouch = false;


    void Start()
    {
        m_MoveObj = m_PlayerObj.GetComponent<PlayerCharacter>().GetSkillRange();
        m_rectBack = m_BackObj.GetComponent<RectTransform>();
        m_rectJoystick = m_JoystickObj.GetComponent<RectTransform>();
        m_fRadius = m_rectBack.rect.width * 0.5f;
    }

    void Update()
    {
        if (m_PlayerObj)
        {
            if (m_bTouch && m_MoveObj)
            {
                m_MoveObj.transform.position = m_vecMove;

                //if (m_MoveObj.activeSelf)
                //    ChangePoint();
            }
            m_JoystickObj.SetActive(m_bTouch);
            m_MoveObj.SetActive(m_bTouch);
        }
    }

    void FindMonster()
    {
        int nLayer = 1 << LayerMask.NameToLayer("Monster");
        Collider[] m_TargetFind = Physics.OverlapSphere(m_PlayerObj.transform.position, m_fRadius * 0.09f /*이거 거리는 어떡하지*/, nLayer);
        // 위치 거리에 따라 가장 가까운 오브젝트에 따라가기
        for (int i = 0; i < m_TargetFind.Length; i++)
        {
            if (m_TargetFind[i])
            {
                m_TargetTransform.Add(m_TargetFind[i].transform);
                Debug.Log($"Find Target {m_TargetFind[i]}");
            }
        }
    }

    void ChangePoint()
    {
        if (m_TargetTransform != null)
        {
            for (int i = 0; i < m_TargetTransform.Count - 1; i++)
            {
                Debug.Log($"TargetTransform : { m_TargetTransform.Count}");
                if ((m_PlayerObj.transform.position - m_TargetTransform[i].position).magnitude < (m_PlayerObj.transform.position - m_TargetTransform[i + 1].position).magnitude)
                {
                    TargetTF = m_TargetTransform[i];
                }
                else
                {
                    TargetTF = m_TargetTransform[i + 1];
                }
            }

            if (TargetTF != null)
            {
                m_MoveObj.transform.localPosition += TargetTF.transform.position;
            }
            m_TargetTransform.Clear();
        }
    }

    void OnTouch(Vector2 vecTouch)
    {
        Vector2 vec = new Vector2(vecTouch.x - m_rectBack.position.x, vecTouch.y - m_rectBack.position.y);

        // vec값을 m_fRadius 이상이 되지 않도록 합니다.
        vec = Vector2.ClampMagnitude(vec, m_fRadius);
        m_rectJoystick.localPosition = vec;

        // 조이스틱 배경과 조이스틱과의 거리 비율로 이동합니다.
        float fSqr = (m_rectBack.position - m_rectJoystick.position).sqrMagnitude / (m_fRadius * m_fRadius);

        // 터치위치 정규화
        Vector2 vecNormal = vec.normalized;

        if (m_MoveObj)
        {
            m_vecMove = new Vector3(vecNormal.x * m_Range * fSqr, 0f, vecNormal.y * m_Range * fSqr) + m_PlayerObj.transform.position;
            m_MoveObj.transform.eulerAngles = new Vector3(0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg, 0f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        m_bTouch = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        //FindMonster();
        m_bTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 원래 위치로 되돌립니다.
        m_rectJoystick.localPosition = Vector2.zero;
        m_bTouch = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(m_PlayerObj.transform.position, m_fRadius * 0.09f);
    }
}
