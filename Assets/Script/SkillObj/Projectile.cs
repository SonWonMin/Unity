using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Skill
{
    Vector3 vPosition;
    [SerializeField]
    float Range;
    public GameObject hit;
    public Status m_Caster = null;

    void Start()
    {
        vPosition = this.gameObject.transform.position;
    }

    void Update()
    {
        Vector3 vPos = transform.position;
        float fDIST = Vector3.Distance(vPos, vPosition); // Distance 무겁다?
        if(fDIST > Range)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetAttackList(Collider target)
    {
        PlayerCharacter Player = m_Caster.GetComponent<PlayerCharacter>();
        Player.AddMonsterList(target.gameObject);
        Player.SetAttackMonster(target.gameObject);
    }

    public void SetCaster(ref Status caster)
    {
        m_Caster = caster;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Caster In");

        if (m_Caster && other.tag != "Item")
        {
            if (other.CompareTag("Wall"))  // 벽에 충돌 시
            {
                Destroy(this.gameObject);
            }
            else if (m_Caster.gameObject.CompareTag("Player") && other.tag != "Player" && this.gameObject.tag != other.tag)  // 사용자가 플레이어일때
            {
                PhysicalAttack(ref m_Caster, other.gameObject);
                SetAttackList(other);
                Destroy(this.gameObject);
            }
            else if (m_Caster.gameObject.CompareTag("Monster") && other.tag != "Monster" && this.gameObject.tag != other.tag)  // 사용자가 몬스터일때
            {
                PhysicalAttack(ref m_Caster, other.gameObject);
                Destroy(this.gameObject);
            }



            if (m_Caster.gameObject.tag != other.tag && this.gameObject.tag != other.tag)  // 충돌 시 충돌 파티클 생성
            {
                if (hit)
                {
                    GameObject HitInstance = Instantiate(hit, transform.position, Quaternion.identity);
                    ParticleSystem HitPs = HitInstance.GetComponent<ParticleSystem>();
                    Destroy(HitInstance, HitPs.main.duration);
                }
            }
        }
    }
}
