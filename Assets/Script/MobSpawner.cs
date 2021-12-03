using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField]
    int m_Max_Obj_Count = 0;
    [SerializeField]
    float m_CreateCycle = 0;

    float m_Cycle = 0;

    [SerializeField]
    GameObject m_CreateObj;
    [SerializeField]
    List<GameObject> Created_Obj;

    Vector3 m_SpawnRange;  // 몬스터 생성 위치 설정 (랜덤값으로 일정한 범위 내에서 생성된다. LocalPosition)


    void Update()
    {
        if(m_CreateObj)
        {
            m_Cycle += Time.deltaTime;
            if(m_CreateCycle < m_Cycle)
            {
                if (m_Max_Obj_Count > Created_Obj.Count)
                {
                    Created_Obj.Add(Instantiate(m_CreateObj, transform.position, Quaternion.identity));
                    m_Cycle = 0;
                }
            }
        }
    }
}
