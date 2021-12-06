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

    [SerializeField]
    float m_SpawnRange;  // 몬스터 생성 위치 설정 (랜덤값으로 일정한 범위 내에서 생성된다. LocalPosition)
    [SerializeField]
    float m_StartRange;
    [SerializeField]
    float m_MaxRange;


    void Update()
    {
        SpawnMonster();
        DeleteMonsterList();
    }

    public void SpawnMonster()
    {
        if (m_CreateObj)
        {
            m_Cycle += Time.deltaTime;

            if (m_CreateCycle < m_Cycle)
            {
                if (m_Max_Obj_Count > Created_Obj.Count)
                {
                    SetSpawnPoint();
                    GameObject CreateObj = Instantiate(m_CreateObj, new Vector3(transform.position.x + m_SpawnRange, transform.position.y, transform.position.z + m_SpawnRange), m_CreateObj.transform.rotation);
                    Created_Obj.Add(CreateObj);
                    m_Cycle = 0;
                }
            }
        }
    }

    public void DeleteMonsterList()
    {
        for(int i = 0; i < Created_Obj.Count; i++)
        {
            if(Created_Obj[i] == null)
            {
                Created_Obj.Remove(Created_Obj[i]);
            }
        }
    }

    public void SetSpawnPoint()
    {
        m_SpawnRange = Random.Range(m_StartRange, m_MaxRange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, m_MaxRange);
    }
}
