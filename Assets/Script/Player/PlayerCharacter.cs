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
    SkillSetting m_PlayerSkill_Setting;
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

    public enum CharacterState{ IDLE, WALK, RUN, ATTACK, SKILL_1, SKILL_2, SKILL_3, HIT };
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
        StateUpdate();
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
            case CharacterState.HIT:
                AnimationSetting("Hit");
                break;
            case CharacterState.SKILL_1:
                AnimationSetting("SKILL1");
                break;
            case CharacterState.SKILL_2:
                AnimationSetting("SKILL2");
                break;
            case CharacterState.SKILL_3:
                AnimationSetting("SKILL3");
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
                PlayerAnimation.SetBool("Idle", true);
                PlayerAnimation.SetBool("Walk", false);
                PlayerAnimation.SetBool("Run", false);
                PlayerAnimation.SetBool("Attack", false);
                PlayerAnimation.SetBool("Hit", false);
                break;
            case "WALK":
                PlayerAnimation.SetBool("Idle", false);
                PlayerAnimation.SetBool("Walk", true);
                PlayerAnimation.SetBool("Run", false);
                PlayerAnimation.SetBool("Attack", false);
                PlayerAnimation.SetBool("Hit", false);
                break;
            case "RUN":
                PlayerAnimation.SetBool("Idle", false);
                PlayerAnimation.SetBool("Walk", false);
                PlayerAnimation.SetBool("Run", true);
                PlayerAnimation.SetBool("Attack", false);
                PlayerAnimation.SetBool("Hit", false);
                break;
            case "Hit":
                PlayerAnimation.SetBool("Idle", false);
                PlayerAnimation.SetBool("Walk", false);
                PlayerAnimation.SetBool("Run", false);
                PlayerAnimation.SetBool("Attack", false);
                PlayerAnimation.SetBool("Hit", true);
                break;
            case "ATTACK":
                StartCoroutine(Attack_Animation());
                break;
            case "SKILL1":
                StartCoroutine(Skill_1_Animation());
                break;
            case "SKILL2":
                StartCoroutine(Skill_2_Animation());
                break;
            case "SKILL3":
                StartCoroutine(Skill_3_Animation());
                break;
        }
    }

    public void SetcurState(CharacterState state)
    {
        m_curState = state;
    }

    public float GetPlayerStatus(string Status) // 현재 플레이어의 상태를 지정한다. 이거 아닌데
    {
        return m_PlayerStatus.GetStatus(Status);
    }

    public void SetPlayerStatus(string Status)
    {
        switch(Status)
        {
            case "IDLE":
                m_curState = CharacterState.IDLE;
                break;
            case "WALK":
                m_curState = CharacterState.WALK;
                break;
            case "RUN":
                m_curState = CharacterState.RUN;
                break;
            case "ATTACK":
                m_curState = CharacterState.ATTACK;
                break;
            case "SKILL1":
                m_curState = CharacterState.SKILL_1;
                break;
            case "SKILL2":
                m_curState = CharacterState.SKILL_2;
                break;
            case "SKILL3":
                m_curState = CharacterState.SKILL_3;
                break;
            case "Hit":
                m_curState = CharacterState.HIT;
                break;
        }
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
            SetPlayerStatus("ATTACK");
        }
    }

    public Inventory ReturnPlayerInventory()
    {
        return m_PlayerInventory;
    }

    public void PlayerSkill_1() // 투사체 발사
    {
        if (m_PlayerSkill.MultiRangeAttack(ref m_PlayerStatus))
            SetPlayerStatus("SKILL1");
    }

    public void PlayerSkill_2() // 범위 공격
    {
        if(m_PlayerSkill.RangedAttack(ref m_PlayerStatus, m_SkillRange.transform))
            SetPlayerStatus("SKILL2");
    }

    public void PlayerSkill_3() // 얼음 범위공격
    {
        if(m_PlayerSkill.IceRangeAttack(ref m_PlayerStatus))
            SetPlayerStatus("SKILL3");
    }

    public IEnumerator Attack_Animation()
    {
        PlayerAnimation.SetBool("Attack", true);
        yield return new WaitForSeconds(0.1f);
        PlayerAnimation.SetBool("Attack", false);
        m_curState = CharacterState.IDLE;
    }

    public IEnumerator Skill_1_Animation()
    {
        PlayerAnimation.SetBool("Skill_1", true);
        yield return new WaitForSeconds(0.1f);
        PlayerAnimation.SetBool("Skill_1", false);
        m_curState = CharacterState.IDLE;
    }

    public IEnumerator Skill_2_Animation()
    {
        PlayerAnimation.SetBool("Skill_2", true);
        yield return new WaitForSeconds(0.1f);
        PlayerAnimation.SetBool("Skill_2", false);
        m_curState = CharacterState.IDLE;
    }

    public IEnumerator Skill_3_Animation()
    {
        PlayerAnimation.SetBool("Skill_3", true);
        yield return new WaitForSeconds(0.1f);
        PlayerAnimation.SetBool("Skill_3", false);
        m_curState = CharacterState.IDLE;
    }
}
