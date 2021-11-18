using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject m_MonsterObj;
    [SerializeField]
    Status m_MonsterStatus;
    [SerializeField]
    Skill m_MonsterSkill;
    SkillSetting m_MonsterSkillSetting;
    [SerializeField]
    DropItemList m_MonsterDropItem;

    enum MonsterState { IDLE, CHASE, ATTACK };  // 대기상태, 추적상태, 공격상태.
    MonsterState curState;

    Collider[] m_TargetFind;

    void Awake()
    {
        MonsterInit();

        if (m_MonsterObj == null)
            m_MonsterObj = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        TargetChase();
        TargetAttack();
        StateUpdate();
    }
    
    void MonsterInit()
    {
        m_MonsterStatus = gameObject.GetComponent<Status>();
        m_MonsterSkill = gameObject.GetComponent<Skill>();
        m_MonsterSkillSetting = gameObject.GetComponent<SkillSetting>();
        m_MonsterDropItem = gameObject.GetComponent<DropItemList>();
        curState = MonsterState.IDLE;
    }

    void StateUpdate()
    {
        switch(curState)
        {
            case MonsterState.IDLE:
                break;
            case MonsterState.CHASE:
                break;
            case MonsterState.ATTACK:
                m_MonsterStatus.SetStatus("Move_Speed", 0);
                break;
        }
    }

    void TargetChase()
    {
        int nLayer = 1 << LayerMask.NameToLayer("Player");
        m_TargetFind = Physics.OverlapSphere(transform.position,
             m_MonsterStatus.GetStatus("ChaseRange"), nLayer);

        for (int i = 0; i < m_TargetFind.Length; i++)
        {
            if(m_TargetFind[i])
            {
                curState = MonsterState.CHASE;
                Vector3 TargetPosition = new Vector3(m_TargetFind[i].transform.position.x, m_MonsterObj.transform.position.y, m_TargetFind[i].transform.position.z);
                transform.LookAt(TargetPosition);
                MoveTest(m_TargetFind[i].transform);
            }
            else
            {
                curState = MonsterState.IDLE;
            }
        }
    }

    void TargetAttack()
    {
        int nLayer = 1 << LayerMask.NameToLayer("Player");
        m_TargetFind = Physics.OverlapSphere(transform.position,
             m_MonsterStatus.GetStatus("AttackRange"), nLayer);

        for (int i = 0; i < m_TargetFind.Length; i++)
        {
            if (m_TargetFind[i])
            {
                curState = MonsterState.ATTACK;
                Vector3 TargetPosition = new Vector3(m_TargetFind[i].transform.position.x, m_MonsterObj.transform.position.y, m_TargetFind[i].transform.position.z);
                transform.LookAt(TargetPosition);  // (만약 쓴다면 lerp 였나 부드러운 움직임 쓰는것도 좋을듯)
                float nTime = m_MonsterStatus.GetStatus("ATK_Speed");
                StartCoroutine(AttackState(m_TargetFind[i], nTime));
            }
            else
            {
                curState = MonsterState.IDLE;
            }
        }
    }

    void MoveTest(Transform Target)
    {
        Vector3 vTargetPos = Target.transform.position;  // 타켓의 현재 좌표값을 받아 저장한다.
        Vector3 vPos = this.transform.position;  // 나 자신의 현재 좌표값을 받아 저장한다.
        Vector3 vDist = vTargetPos - vPos;  // 타겟과 나 사이의 거리를 저장한다.
        Vector3 vDir = vDist.normalized;  // 거리의 방향을 저장한다.
        vDir.y = 0;  // y축 움직임 0

        float fDist = vDist.magnitude;  // 사이 거리 벡터의 거리(길이)를 반환한다.

        if (fDist > m_MonsterStatus.GetStatus("AttackRange"))  // 타겟과의 거리가 나 자신의 공격범위보다 먼 경우 실행
        {
            this.transform.position += vDir * m_MonsterStatus.GetStatus("Move_Speed") * Time.deltaTime;  // 현재 포지션을 일정한 속도로 이동
        }
    }

    public IEnumerator AttackState(Collider target, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(m_MonsterSkillSetting.GetMagicLevel(0) > 0)
        {
            m_MonsterSkill.MultiRangeAttack(ref m_MonsterStatus);
        }
        else if((m_MonsterSkillSetting.GetMagicLevel(1) > 0))
        {
            m_MonsterSkill.RangedAttack(ref m_MonsterStatus, target.transform); 
        }
        else if ((m_MonsterSkillSetting.GetMagicLevel(2) > 0))
        {
            m_MonsterSkill.IceRangeAttack(ref m_MonsterStatus);
        }
        m_MonsterStatus.SetStatus("Move_Speed", m_MonsterStatus.GetOriginalStatus("Move_Speed"));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, m_MonsterStatus.GetStatus("ChaseRange"));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, m_MonsterStatus.GetStatus("AttackRange"));
    }
}
