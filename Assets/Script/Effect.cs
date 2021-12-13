using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public void ProjectileEffect(GameObject projectile, ref Status caster, float power, int ProjectileCount = 1, float angle = 0)  // 투사체, 사용자, 힘, 투사체 개수
    {
        List<GameObject> CopyProjectileObj = new List<GameObject>();
        List<Rigidbody> CopyProjectileRigid = new List<Rigidbody>();

        for (int i = 0; i < ProjectileCount; i++)
        {
            CopyProjectileObj.Add(Instantiate(projectile, caster.transform.position + caster.transform.forward, caster.transform.rotation));  // 복사말고 필요한 개수만큼 가져오는걸로

            if (i >= 1)
            {
                if(i % 2 == 1)
                {
                    CopyProjectileObj[i].transform.rotation = Quaternion.Euler(CopyProjectileObj[0].transform.rotation.eulerAngles.x, CopyProjectileObj[i-1].transform.rotation.eulerAngles.y + (i * angle), CopyProjectileObj[0].transform.rotation.eulerAngles.z);
                }
                else
                {
                    CopyProjectileObj[i].transform.rotation = Quaternion.Euler(CopyProjectileObj[0].transform.rotation.eulerAngles.x, CopyProjectileObj[i-1].transform.rotation.eulerAngles.y + (i * -angle), CopyProjectileObj[0].transform.rotation.eulerAngles.z);
                }
                
            }

            Rigidbody rigid = CopyProjectileObj[i].GetComponent<Rigidbody>();
            CopyProjectileRigid.Add(rigid);
            CopyProjectileRigid[i].AddForce(CopyProjectileObj[i].transform.forward * power);

        }

    }

    public void RangeAttackEffect(Vector3 skill_position, GameObject attackObj, float attack_Delay, float duration_Time, float multiple = 1) // 범위 공격 테스트, 일단 이렇게 해두고 이후에 해보면서 추가하기 스킬 범위까지 추가하기
    {
        GameObject AttackObj = Instantiate(attackObj, skill_position, Quaternion.identity);
        RangeObj rangeComponent = AttackObj.GetComponent<RangeObj>();
        rangeComponent.m_Multiple = multiple;
        rangeComponent.SetTime(attack_Delay, duration_Time);
    }

    public void PhysicalAttack(ref Status caster, GameObject target, float multiple = 1)
    {
        Status casterStatus = caster.GetComponent<Status>();
        Status targetStatus = target.GetComponent<Status>();

        if (targetStatus)
        {
            float Damage = (casterStatus.GetStatus("Physical_ATK") * multiple) - targetStatus.GetStatus("Defence");
            if (Damage < 0)
                Damage = 1;
            targetStatus.SetStatus("HP", Damage, "-");
        }
    }

    public void MagicAttack(ref Status caster, GameObject target, float multiple = 1)  // 스킬 사용자, 타겟 오브젝트, 공격 배수
    {
        Status casterStatus = caster.GetComponent<Status>();
        Status targetStatus = target.GetComponent<Status>();

        if (targetStatus)
        {
            float Damage = (casterStatus.GetStatus("Magic_ATK") * multiple) - targetStatus.GetStatus("Defence");
            if (Damage < 0)
                Damage = 1;
            targetStatus.SetStatus("HP", Damage, "-");
        }
    }

    public void Target_HitMotion(GameObject target)
    {
        if (target.CompareTag("Monster"))
        {
            Monster monster = target.GetComponent<Monster>();
            monster.SetCurState(Monster.MonsterState.HIT);
        }
        else if(target.CompareTag("Player"))
        {
            PlayerCharacter player = target.GetComponent<PlayerCharacter>();
            player.SetcurState(PlayerCharacter.CharacterState.HIT);
        }

    }
}
