using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeObj : Skill
{
    [SerializeField]
    float m_Radius;

    public Status m_Caster = null;

    public float m_Multiple = 1;

    public float m_Attack_Delay = 0f;
    public float m_Duration_Time = 0f;
    float m_Attack_Time_Check = 0f;
    float m_Duration_Time_Check = 0f;

    public List<GameObject> GetInObj;

    // Update is called once per frame
    void Update()
    {
        TimeCheck();
        ChangeRadius();
        DestroyCheck();
        DeleteListObj();

        if(m_Caster)
            AttackListObj();
    }

    public void SetCaster(ref Status caster)
    {
        m_Caster = caster;
    }

    void TimeCheck()
    {
        m_Attack_Time_Check -= Time.deltaTime;
        m_Duration_Time_Check -= Time.deltaTime;

        if (m_Attack_Time_Check < 0)
            m_Attack_Time_Check = 0;

        if (m_Duration_Time_Check < 0)
            m_Duration_Time_Check = 0;
    }

    public void SetTime(float attack, float duration)
    {
        m_Attack_Delay = attack;
        m_Attack_Time_Check = 0;
        m_Duration_Time_Check = duration;
    }

    void ChangeRadius()
    {
        CapsuleCollider Range = this.gameObject.GetComponent<CapsuleCollider>();
        Range.radius = m_Radius;
    }

    void DestroyCheck()
    {
        if (m_Duration_Time_Check <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void AttackListObj()
    {
        if (m_Attack_Time_Check <= 0)
        {
            for (int i = 0; i < GetInObj.Count; i++)
            {
                if (GetInObj[i])
                {
                    MagicAttack(ref m_Caster, GetInObj[i].gameObject, m_Multiple);
                    m_Attack_Time_Check = m_Attack_Delay;
                }
            }
        }
    }

    void DeleteListObj()
    {
        for (int i = 0; i < GetInObj.Count; i++)
        {
            if (GetInObj[i] == null)
            {
                GetInObj.Remove(GetInObj[i]);
            }
        }
    }

    public void SetAttackList(Collider target)
    {
        PlayerCharacter Player = m_Caster.GetComponent<PlayerCharacter>();
        Player.AddMonsterList(target.gameObject);
        Player.SetAttackMonster(target.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Caster In");
        Debug.Log($"Ohter Tag {other.gameObject.tag}");

        if (m_Caster && other.tag != "Item")
        {
            if (m_Caster.gameObject.CompareTag("Player") && other.tag != "Player")
            {
                GetInObj.Add(other.gameObject);
                SetAttackList(other);
            }
            else if (m_Caster.gameObject.CompareTag("Monster") && other.tag != "Monster")
            {
                GetInObj.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GetInObj.Remove(other.gameObject);
    }
}
