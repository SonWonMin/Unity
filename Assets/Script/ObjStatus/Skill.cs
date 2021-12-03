using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Effect
{
    [SerializeField]
    static Status m_Caster = null;
    SkillSetting m_Caster_Skill = null;

    private void Start()
    {
        m_Caster_Skill = this.gameObject.GetComponent<SkillSetting>();
    }

    public void NormalRangeAttack(ref Status status)
    {
        if (status.gameObject.activeSelf)
        {
            m_Caster = status;
            Debug.Log(m_Caster);
            GameObject CopyObj = ProjectileManager.Getinstance().GetPrefab(0);
            Projectile ProjectileObj = CopyObj.GetComponent<Projectile>();
            ProjectileObj.SetCaster(ref m_Caster);
            ProjectileEffect(CopyObj, ref m_Caster, 500);
            // 투사체의 위치 caster 의 위치로 설정
            // forward 로 일정함 힘으로 이동  // 일정 거리 이동 시 파괴
        }
    }

    public bool MultiRangeAttack(ref Status status, int ProjectileCount = 1, float coolTime = 0.5f)
    {
        int level = m_Caster_Skill.GetMagicLevel(0);

        if (status.gameObject.activeSelf && level > 0 )
        {
            m_Caster = status;
            Debug.Log(m_Caster);
            if (status.GetStatus("Cur_MP") >= 20 && m_Caster_Skill.GetCoolTime("MultiRangeAttack") <= 0)  // 임시 마나 소모량 체크. 이후에 마나 소모량 각각 추가할 것
            {
                m_Caster_Skill.SetCoolTime("MultiRangeAttack", coolTime);
                GameObject CopyObj = ProjectileManager.Getinstance().GetPrefab(1);
                Projectile ProjectileObj = CopyObj.GetComponent<Projectile>();
                ProjectileObj.SetCaster(ref m_Caster);
                ProjectileEffect(CopyObj, ref m_Caster, 500, ProjectileCount * level, 10);
                status.SetStatus("MP", 20, "-");  // 설정 스텟, 수치, 연산
                return true;
            }
            // 투사체의 위치 caster 의 위치로 설정
            // forward 로 일정함 힘으로 이동  // 일정 거리 이동 시 파괴
        }
        return false;
    }

    public void RangedAttack(ref Status status, GameObject target) // 타겟의 위치에 스킬 사용
    {
        int level = m_Caster_Skill.GetMagicLevel(1);

        if (status.gameObject.activeSelf && level > 0 )
        {
            m_Caster = status;

            if (status.GetStatus("Cur_MP") >= 50 && m_Caster_Skill.GetCoolTime("IceRangeAttack") <= 0)
            {
                m_Caster_Skill.SetCoolTime("RangedAttack", 5);
                GameObject CopyObj = ProjectileManager.Getinstance().GetPrefab(2);
                RangeObj ProjectileObj = CopyObj.GetComponent<RangeObj>();
                ProjectileObj.SetCaster(ref m_Caster);
                RangeAttackEffect(target.transform.position, CopyObj, 0.5f, 3.5f, 1.1f * level);
                status.SetStatus("MP", 50, "-");
            }
        }
    }

    public bool RangedAttack(ref Status status, Transform skill_position)  // 스킬 범위 위치에 스킬 사용
    {
        int level = m_Caster_Skill.GetMagicLevel(1);

        if (status.gameObject.activeSelf && level > 0 )
        {
            m_Caster = status;

            if (status.GetStatus("Cur_MP") >= 50 && m_Caster_Skill.GetCoolTime("RangedAttack") <= 0)
            {
                m_Caster_Skill.SetCoolTime("RangedAttack", 5);
                GameObject CopyObj = ProjectileManager.Getinstance().GetPrefab(2);
                RangeObj ProjectileObj = CopyObj.GetComponent<RangeObj>();
                ProjectileObj.SetCaster(ref m_Caster);
                RangeAttackEffect(skill_position.position, CopyObj, 0.5f, 3.5f, 1.1f * level);
                status.SetStatus("MP", 50, "-");
                return true;
            }
        }
        return false;
    }

    public bool IceRangeAttack(ref Status status) // 사용자의 위치에 스킬 사용
    {
        int level = m_Caster_Skill.GetMagicLevel(2);

        if (status.gameObject.activeSelf && level > 0 )
        {
            m_Caster = status;

            if (status.GetStatus("Cur_MP") >= 50 && m_Caster_Skill.GetCoolTime("IceRangeAttack") <= 0)
            {
                m_Caster_Skill.SetCoolTime("IceRangeAttack", 3);
                GameObject CopyObj = ProjectileManager.Getinstance().GetPrefab(3);
                RangeObj ProjectileObj = CopyObj.GetComponent<RangeObj>();
                ProjectileObj.SetCaster(ref m_Caster);
                RangeAttackEffect(new Vector3(status.transform.position.x, status.transform.position.y - 1, status.transform.position.z), CopyObj, 2.0f, 2.0f, 5.0f);
                status.SetStatus("MP", 50, "-");
                return true;
            }
        }
        return false;
    }

    public ref Status GetCaster()
    {
        return ref m_Caster;
    }

}
