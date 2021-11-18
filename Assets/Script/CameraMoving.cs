using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    // 테스트 주석
    [SerializeField]
    GameObject m_TargetObj;  // 카메라 타겟 오브젝트
    [SerializeField]
    float m_Distance = 5;  // 타겟과의 사이 거리
    [SerializeField]
    float m_Height = 10;  // 타겟과의 사이 높이
    Transform m_Tr;  // 내 위치

    // Start is called before the first frame update
    void Start()
    {
        m_Tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }

    void ChasePlayer()
    {
        if (m_TargetObj)
        {
            m_Tr.position = m_TargetObj.transform.position - (1 * Vector3.forward * m_Distance) + (Vector3.up * m_Height);
            m_Tr.LookAt(m_TargetObj.transform.position);
        }
    }
}
