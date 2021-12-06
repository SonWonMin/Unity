using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    struct ObjStatus  // 원본 능력치
    {
        public float Lv;

        public float Max_HP;
        public float Max_MP;
        public float Physical_ATK;
        public float Magic_ATK;
        public float ATK_Speed;
        public float Defence;
        public float Move_Speed;
        public float HP_Recovery_Sec;
        public float MP_Recovery_Sec;
        public float Max_Exp;

        public float ChaseRange;
        public float AttackRange;

        public float SkillPoint;
        public float StatPoint;
    }

    struct StatusLevel
    {
        public int Strength;
        public int Int;
        public int Max_HP;
        public int Max_MP;
    }
    [SerializeField]
    string Name;
    [SerializeField]
    float LV;
    [SerializeField]  // 변동 능력치 (실 게임에 적용)
    float m_HP;  // 현재 체력
    [SerializeField]
    float m_MP;  // 현재 마나
    [SerializeField]
    float m_Physical_ATK;  // 물리공격력
    [SerializeField]
    float m_Magic_ATK;  // 마법공격력
    [SerializeField]
    float m_ATK_Speed;  // 공격속도
    [SerializeField]
    float m_Defence;  // 방어력
    [SerializeField]
    float m_Move_Speed;  // 이동속도
    [SerializeField]
    float m_HP_Recovery_sec = 0;  // 체력 회복량
    [SerializeField]
    float m_MP_Recovery_sec = 0;  // 마나 회복량
    [SerializeField]
    float Cur_Exp;

    ObjStatus Stat = new ObjStatus();
    StatusLevel StatLV = new StatusLevel();

    float m_RecoveryTime = 0;

    // AI 추적, 공격 범위 설정
    [SerializeField]
    float m_ChaseRange;
    [SerializeField]
    float m_AttackRange;

    private void Awake()
    {
        Init();
    }

    void Init()  // 스텟 데이터 초기화
    {
        Stat.Lv = LV;
        Stat.Max_HP = m_HP;
        Stat.Max_MP = m_MP;
        Stat.Physical_ATK = m_Physical_ATK;
        Stat.Magic_ATK = m_Magic_ATK;
        Stat.ATK_Speed = m_ATK_Speed;
        Stat.Defence = m_Defence;
        Stat.Move_Speed = m_Move_Speed;
        Stat.HP_Recovery_Sec = m_HP_Recovery_sec;
        Stat.MP_Recovery_Sec = m_MP_Recovery_sec;
        Stat.ChaseRange = m_ChaseRange;
        Stat.AttackRange = m_AttackRange;
        Stat.Max_Exp = Cur_Exp;
        Stat.SkillPoint = 3;
        Stat.StatPoint = 5;

        StatLV.Strength = 1;
        StatLV.Int = 1;
        StatLV.Max_HP = 1;
        StatLV.Max_MP = 1;

        if (this.gameObject.tag == "Player")
            Cur_Exp = 0;
    }

    void Recovery()  // 체력, 마나 회복
    {
        m_RecoveryTime += Time.deltaTime;

        if (m_RecoveryTime >= 1)
        {
            m_RecoveryTime = 0;

            if (m_HP < Stat.Max_HP)
            {
                m_HP += m_HP_Recovery_sec;

                if (m_HP > Stat.Max_HP)
                {
                    m_HP = Stat.Max_HP;
                }
            }
            if (m_MP < Stat.Max_MP)
            {
                m_MP += m_MP_Recovery_sec;

                if (m_MP > Stat.Max_MP)
                {
                    m_MP = Stat.Max_MP;
                }
            }
        }
    }

    public void AddStatusLv(string stat)
    {
        if (Stat.StatPoint > 0)
        {
            switch (stat)
            {
                case "Strength":
                    StatLV.Strength++;
                    Stat.StatPoint--;
                    SetStatus("Physical_ATK", m_Physical_ATK + (StatLV.Strength * 1.2f), "=");  // 배수에 맞게 능력치 적용
                    break;
                case "Int":
                    StatLV.Int++;
                    Stat.StatPoint--;
                    SetStatus("Magic_ATK", m_Magic_ATK + (StatLV.Int * 1.4f), "=");
                    break;
                case "Max_HP":
                    StatLV.Max_HP++;
                    Stat.StatPoint--;
                    SetStatus("Max_HP", Stat.Max_HP + (StatLV.Max_HP * 10f), "=");
                    break;
                case "Max_MP":
                    StatLV.Max_MP++;
                    Stat.StatPoint--;
                    SetStatus("Max_MP", Stat.Max_MP + (StatLV.Max_MP * 10f), "=");
                    break;
            }
        }
    }

    public int ReturnStatusLv(string stat)
    {
        switch (stat)
        {
            case "Strength":
                return StatLV.Strength;
            case "Int":
                return StatLV.Int;
            case "Max_HP":
                return StatLV.Max_HP;
            case "Max_MP":
                return StatLV.Max_MP;
            default:
                return 0;
        }
    }

    public void SetStatus(float hp, float mp, float physical_atk, float magic_atk, float atk_speed, float defence, float move_speed, float HP_recovery, float MP_recovery)  // 능력치 설정 임시 함수
    {
        m_HP = hp;
        m_MP = mp;
        m_Physical_ATK = physical_atk;
        m_Magic_ATK = magic_atk;
        m_ATK_Speed = atk_speed;
        m_Defence = defence;
        m_Move_Speed = move_speed;
        m_HP_Recovery_sec = HP_recovery;
        m_MP_Recovery_sec = MP_recovery;
    }

    public void SetStatus(string stat, float number, string cal = "=")  // 스텟 설정 (스텟 명, 값, 연산)
    {
        switch (cal)
        {
            case "=":
                switch (stat)
                {
                    case "Max_HP":
                        Stat.Max_HP = number;
                        break;
                    case "Max_MP":
                        Stat.Max_MP = number;
                        break;
                    case "HP":
                        m_HP = number;
                        break;
                    case "MP":
                        m_MP = number;
                        break;
                    case "Physical_ATK":
                        m_Physical_ATK = number;
                        break;
                    case "Magic_ATK":
                        m_Magic_ATK = number;
                        break;
                    case "ATK_Speed":
                        m_ATK_Speed = number;
                        break;
                    case "Defence":
                        m_Defence = number;
                        break;
                    case "Move_Speed":
                        m_Move_Speed = number;
                        break;
                    case "HPRecovery":
                        m_HP_Recovery_sec = number;
                        break;
                    case "MPRecovey":
                        m_MP_Recovery_sec = number;
                        break;
                    case "ChaseRange":
                        m_ChaseRange = number;
                        break;
                    case "AttackRange":
                        m_AttackRange = number;
                        break;
                    case "EXP":
                        Cur_Exp = number;
                        break;
                    case "SkillPoint":
                        Stat.SkillPoint = number;
                        break;
                    case "StatPoint":
                        Stat.StatPoint = number;
                        break;
                    default:
                        break;
                }
                break;
            case "-":
                switch (stat)
                {
                    case "Max_HP":
                        Stat.Max_HP -= number;
                        break;
                    case "Max_MP":
                        Stat.Max_MP -= number;
                        break;
                    case "HP":
                        m_HP -= number;
                        break;
                    case "MP":
                        m_MP -= number;
                        break;
                    case "Physical_ATK":
                        m_Physical_ATK -= number;
                        break;
                    case "Magic_ATK":
                        m_Magic_ATK -= number;
                        break;
                    case "ATK_Speed":
                        m_ATK_Speed -= number;
                        break;
                    case "Defence":
                        m_Defence -= number;
                        break;
                    case "Move_Speed":
                        m_Move_Speed -= number;
                        break;
                    case "HPRecovery":
                        m_HP_Recovery_sec -= number;
                        break;
                    case "MPRecovey":
                        m_MP_Recovery_sec -= number;
                        break;
                    case "ChaseRange":
                        m_ChaseRange -= number;
                        break;
                    case "AttackRange":
                        m_AttackRange -= number;
                        break;
                    case "EXP":
                        Cur_Exp -= number;
                        break;
                    case "SkillPoint":
                        Stat.SkillPoint -= number;
                        break;
                    case "StatPoint":
                        Stat.StatPoint -= number;
                        break;
                    default:
                        break;
                }
                break;
            case "+":
                switch (stat)
                {
                    case "Max_HP":
                        Stat.Max_HP += number;
                        break;
                    case "Max_MP":
                        Stat.Max_MP += number;
                        break;
                    case "HP":
                        m_HP += number;
                        break;
                    case "MP":
                        m_MP += number;
                        break;
                    case "Physical_ATK":
                        m_Physical_ATK += number;
                        break;
                    case "Magic_ATK":
                        m_Magic_ATK += number;
                        break;
                    case "ATK_Speed":
                        m_ATK_Speed += number;
                        break;
                    case "Defence":
                        m_Defence += number;
                        break;
                    case "Move_Speed":
                        m_Move_Speed += number;
                        break;
                    case "HPRecovery":
                        m_HP_Recovery_sec = number;
                        break;
                    case "MPRecovey":
                        m_MP_Recovery_sec = number;
                        break;
                    case "ChaseRange":
                        m_ChaseRange += number;
                        break;
                    case "AttackRange":
                        m_AttackRange += number;
                        break;
                    case "EXP":
                        Cur_Exp += number;
                        break;
                    case "SkillPoint":
                        Stat.SkillPoint += number;
                        break;
                    case "StatPoint":
                        Stat.StatPoint += number;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    public float GetStatus(string stat)  // 스텟 받아오기 (스텟 명)
    {
        switch (stat)
        {
            case "Max_HP":
                return Stat.Max_HP;
            case "Max_MP":
                return Stat.Max_MP;
            case "Cur_HP":
                return m_HP;
            case "Cur_MP":
                return m_MP;
            case "Physical_ATK":
                return m_Physical_ATK;
            case "Magic_ATK":
                return m_Magic_ATK;
            case "ATK_Speed":
                return m_ATK_Speed;
            case "Defence":
                return m_Defence;
            case "Move_Speed":
                return m_Move_Speed;
            case "HPRecovery":
                return m_HP_Recovery_sec;
            case "MPRecovey":
                return m_MP_Recovery_sec;
            case "ChaseRange":
                return m_ChaseRange;
            case "AttackRange":
                return m_AttackRange;
            case "EXP":
                return Cur_Exp;
            case "Max_EXP":
                return Stat.Max_Exp;
            case "SkillPoint":
                return Stat.SkillPoint;
            case "StatPoint":
                return Stat.StatPoint;
            default:
                return 0;
        }
    }

    public float GetOriginalStatus(string stat)  // 원본 스탯 받아오기 (스텟 명)
    {
        switch (stat)
        {
            case "HP":
                return Stat.Max_HP;
            case "MP":
                return Stat.Max_MP;
            case "Physical_ATK":
                return Stat.Physical_ATK;
            case "Magic_ATK":
                return Stat.Magic_ATK;
            case "ATK_Speed":
                return Stat.ATK_Speed;
            case "Defence":
                return Stat.Defence;
            case "Move_Speed":
                return Stat.Move_Speed;
            case "HPRecovery":
                return Stat.HP_Recovery_Sec;
            case "MPRecovey":
                return Stat.MP_Recovery_Sec;
            case "ChaseRange":
                return Stat.ChaseRange;
            case "AttackRange":
                return Stat.AttackRange;
            case "Max_EXP":
                return Stat.Max_Exp;
            default:
                return 0;
        }
    }

    public void StatusCopy(Status target)
    {
        m_HP = target.m_HP;
        m_MP = target.m_MP;
        m_Physical_ATK = target.m_Physical_ATK;
        m_Magic_ATK = target.m_Magic_ATK;
        m_ATK_Speed = target.m_ATK_Speed;
        m_Defence = target.m_Defence;
        m_Move_Speed = target.m_Move_Speed;
        m_HP_Recovery_sec = target.m_HP_Recovery_sec;
        m_MP_Recovery_sec = target.m_MP_Recovery_sec;
    }

    public void SetItemStatus(Status target, string cal = "+")
    {
        switch(cal)
        {
            case "+":
                m_HP += target.m_HP;
                m_MP += target.m_MP;
                m_Physical_ATK += target.m_Physical_ATK;
                m_Magic_ATK += target.m_Magic_ATK;
                m_ATK_Speed += target.m_ATK_Speed;
                m_Defence += target.m_Defence;
                m_Move_Speed += target.m_Move_Speed;
                m_HP_Recovery_sec += target.m_HP_Recovery_sec;
                m_MP_Recovery_sec += target.m_MP_Recovery_sec;
                break;
            case "-":
                m_HP -= target.m_HP;
                m_MP -= target.m_MP;
                m_Physical_ATK -= target.m_Physical_ATK;
                m_Magic_ATK -= target.m_Magic_ATK;
                m_ATK_Speed -= target.m_ATK_Speed;
                m_Defence -= target.m_Defence;
                m_Move_Speed -= target.m_Move_Speed;
                m_HP_Recovery_sec -= target.m_HP_Recovery_sec;
                m_MP_Recovery_sec -= target.m_MP_Recovery_sec;
                break;
        }
    }

    public string GetName()
    {
        return Name;
    }

    public void ObjDie()  // 사망 처리
    {
        if (m_HP <= 0)
        {
            if (this.gameObject.tag != "Player" && this.gameObject.tag != "Item" && this.gameObject.tag != "Slot")  // 몬스터 사망처리
            {
                if (this.gameObject)
                {
                    Status PlayerStatus = GameManager.Getinstance().m_Player.GetComponent<Status>();
                    DropItemList MonsterDropItem = this.gameObject.GetComponent<DropItemList>();
                    PlayerStatus.SetStatus("EXP", GetStatus("EXP"), "+");
                    MonsterDropItem.DropItem();
                    Destroy(this.gameObject);
                }
            }

            else if(this.gameObject.tag == "Player")  // 플레이어 사망 처리
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void LevelUp()  // 레벨업
    {
        if(Stat.Max_Exp <= Cur_Exp)
        {
            Stat.Lv++;
            Cur_Exp -= Stat.Max_Exp;
            SetStatus("SkillPoint", 1, "+");
            SetStatus("StatPoint", 1, "+");
        }
    }

    void Update()
    {
        ObjDie();
        Recovery();

        if(this.gameObject.tag == "Player")
            LevelUp();
    }

}
