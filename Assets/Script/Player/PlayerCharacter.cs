using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    Status m_PlayerStatus;
    [SerializeField]
    Skill m_PlayerSkill;
    [SerializeField]
    GameObject m_SkillRange;
    [SerializeField]
    List<GameObject> m_ATK_Monster_List;
    [SerializeField]
    Animator PlayerAnimation;
    GameObject m_ATK_Monster;

    [SerializeField]
    Inventory m_PlayerInventory;

    float m_Time = 0;

    enum CharacterState{ IDLE, WALK, RUN, ATTACK };
    CharacterState m_curState;

    private void Start()
    {
        //PlayerAnimation = this.gameObject.GetComponent<Animator>();
        m_curState = CharacterState.IDLE;
    }

    void Update()
    {
        m_Time += Time.deltaTime;
        RemoveMonsterList();
        //StateUpdate();
    }

    void StateUpdate() // 상태를 업데이트 한다.
    {
        switch(m_curState)
        {
            case CharacterState.IDLE:
                AnimationSetting("IDLE");
                break;
            case CharacterState.WALK:
                AnimationSetting("WALK");
                break;
            case CharacterState.RUN:
                AnimationSetting("RUN");
                break;
            case CharacterState.ATTACK:
                AnimationSetting("ATTACK");
                break;
            default:
                break;
        }
    }

    public void AnimationSetting(string state) // 애니메이션 상태를 세팅한다
    {
        switch(state)
        {
            case "IDLE":
                PlayerAnimation.SetBool("isIdle", true);
                PlayerAnimation.SetBool("isWalk", false);
                PlayerAnimation.SetBool("isRun", false);
                PlayerAnimation.SetBool("isAttack", false);
                break;
            case "WALK":
                PlayerAnimation.SetBool("isIdle", false);
                PlayerAnimation.SetBool("isWalk", true);
                PlayerAnimation.SetBool("isRun", false);
                PlayerAnimation.SetBool("isAttack", false);
                break;
            case "RUN":
                PlayerAnimation.SetBool("isIdle", false);
                PlayerAnimation.SetBool("isWalk", false);
                PlayerAnimation.SetBool("isRun", true);
                PlayerAnimation.SetBool("isAttack", false);
                break;
            case "ATTACK":
                PlayerAnimation.SetBool("isIdle", false);
                PlayerAnimation.SetBool("isWalk", false);
                PlayerAnimation.SetBool("isRun", false);
                PlayerAnimation.SetBool("isAttack", true);
                break;
        }
    }

    public float GetPlayerStatus(string Status) // 현재 플레이어의 상태를 지정한다.
    {
        return m_PlayerStatus.GetStatus(Status);
    }

    public void AddMonsterList(GameObject monster) // 몬스터 리스트에 추가한다.
    {
        int Overlap = 0;

        if (m_ATK_Monster_List.Count != 0)
        {
            for (int i = 0; i < m_ATK_Monster_List.Count; i++)
            {
                if(m_ATK_Monster_List[i] == monster)
                {
                    Overlap++;
                }
            }

            if(Overlap <= 0)
            {
                m_ATK_Monster_List.Add(monster);
            }

        }
        else
        {
            m_ATK_Monster_List.Add(monster);
        }
    }

    public void RemoveMonsterList() // 몬스터 리스트에서 제거한다.
    {
        for (int i = 0; i < m_ATK_Monster_List.Count; i++)
        {
            if (m_ATK_Monster_List[i] == null)
            {
                m_ATK_Monster_List.Remove(m_ATK_Monster_List[i]);
            }
        }
    }

    public GameObject ReturnMonsterList() // 몬스터 리스트를 반환한다.
    {
        if(m_ATK_Monster_List.Count != 0)
            return m_ATK_Monster_List[0];

        return null;
    }

    public GameObject GetSkillRange() // 스킬범위 오브젝트를 반환한다.
    {
        return m_SkillRange;
    }

    public void SetAttackMonster(GameObject monster) // 공격 몬스터를 설정한다.
    {
        m_ATK_Monster = monster;
    }

    public GameObject GetAttackMonster() // 공격 몬스터를 반환한다.
    {
        return m_ATK_Monster;
    }

    public void PlayerAttack() // 일반공격
    {
        if (m_Time > m_PlayerStatus.GetStatus("ATK_Speed"))
        {
            m_Time = 0;
            m_PlayerSkill.NormalRangeAttack(ref m_PlayerStatus);
        }
    }

    public Inventory ReturnPlayerInventory()
    {
        return m_PlayerInventory;
    }

    public void PlayerSkill_1() // 투사체 발사
    {
        m_PlayerSkill.MultiRangeAttack(ref m_PlayerStatus);
    }

    public void PlayerSkill_2() // 범위 공격
    {
        m_PlayerSkill.RangedAttack(ref m_PlayerStatus, m_SkillRange.transform);
    }

    public void PlayerSkill_3() // 얼음 범위공격
    {
        m_PlayerSkill.IceRangeAttack(ref m_PlayerStatus);
    }

}
