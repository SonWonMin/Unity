using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickTest : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField]
    RectTransform m_rectBack;
    [SerializeField]
    RectTransform m_rectJoystick;
    [SerializeField]
    GameObject m_MoveObj;
    PlayerCharacter Player;

    float m_fRadius;  // 반지름
    float m_fSpeed;  // 속도 이후에 플레이어의 속도에 맞게 설정할것.

    Vector3 m_vecMove;  // 이동 좌표 

    bool m_bTouch = false;


    void Start()
    {
        Player = m_MoveObj.GetComponent<PlayerCharacter>();
        m_fSpeed = Player.GetPlayerStatus("Move_Speed");
        m_fRadius = m_rectBack.rect.width * 0.5f;
    }

    void Update()
    {
        if (m_bTouch && m_MoveObj.activeSelf)
        {
            m_MoveObj.transform.position += m_vecMove;
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
            m_vecMove = new Vector3(vecNormal.x * m_fSpeed * Time.deltaTime * fSqr, 0f, vecNormal.y * m_fSpeed * Time.deltaTime * fSqr);
            m_MoveObj.transform.eulerAngles = new Vector3(0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg, 0f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_MoveObj.activeSelf)
        {
            OnTouch(eventData.position);
            Player.GetPlayerStatus("RUN");
            m_bTouch = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_MoveObj.activeSelf)
        {
            OnTouch(eventData.position);
            Player.GetPlayerStatus("IDLE");
            m_bTouch = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 원래 위치로 되돌립니다.
        m_rectJoystick.localPosition = Vector2.zero;
        m_bTouch = false;
    }
}
