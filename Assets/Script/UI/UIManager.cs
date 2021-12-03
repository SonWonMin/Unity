using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public RectTransform m_TestButton;
    public SkillLevelUpTest m_SkillLevelUp;

    [SerializeField]
    GameObject m_MonsterInfo;
    [SerializeField]
    GameObject m_Player; 

    SkillSetting m_PlayerSkill;
    Status m_PlayerStatus;

    [SerializeField]
    Slot m_Select_Slot;
    [SerializeField]
    Status m_Slot_Status;
    public Slot m_temp_Slot;

    public TextMeshProUGUI m_PlayerSkillPoint;
    public TextMeshProUGUI m_PlayerStatusPoint;

    bool input = false;

    public List<GameObject> UI_obj = new List<GameObject>();
    public List<TextMeshProUGUI> StatusInfo_Text = new List<TextMeshProUGUI>();

    public List<GameObject> SkillUI = new List<GameObject>();
    public List<TextMeshProUGUI> Skilllv = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> PlayerStatusLv = new List<TextMeshProUGUI>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        m_SkillLevelUp = this.gameObject.GetComponent<SkillLevelUpTest>();
        m_PlayerSkill = m_Player.GetComponent<SkillSetting>();
        m_PlayerStatus = m_Player.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        if(input == true)
        {
            TestButtonAction();
        }
        else if(input == false)
        {
            TestButtonReturn();
        }

        MonsterInfoActive();
        GetStatusLv();
        GetStatusInfo();

        m_PlayerSkillPoint.text = string.Format($"스킬포인트:{m_PlayerStatus.GetStatus("SkillPoint")}");
        m_PlayerStatusPoint.text = string.Format($"스텟포인트:{m_PlayerStatus.GetStatus("StatPoint")}");
    }
    public static UIManager Getinstance()
    {
        return instance;
    }

    public void SelectSlot(ref Slot slot, Status status)
    {
        m_Select_Slot = slot;
        m_Slot_Status = status;
    }
    
    public Slot ReturnSelectSlot()
    {
        return m_Select_Slot;
    }

    public Status ReturnSelectStatus()
    {
        return m_Slot_Status;
    }

    public void StatusSetUI(string status)
    {
        switch(status)
        {
            case "Strength":
                m_PlayerStatus.AddStatusLv(status);
                break;
            case "Int":
                m_PlayerStatus.AddStatusLv(status);
                break;
            case "Max_HP":
                m_PlayerStatus.AddStatusLv(status);
                break;
            case "Max_MP":
                m_PlayerStatus.AddStatusLv(status);
                break;
        }
    }

    public void GetStatusLv()
    {
        PlayerStatusLv[0].text = string.Format($"힘:{m_PlayerStatus.ReturnStatusLv("Strength")}");
        PlayerStatusLv[1].text = string.Format($"지능:{ m_PlayerStatus.ReturnStatusLv("Int")}");
        PlayerStatusLv[2].text = string.Format($"최대체력:{ m_PlayerStatus.ReturnStatusLv("Max_HP")}");
        PlayerStatusLv[3].text = string.Format($"최대마나:{ m_PlayerStatus.ReturnStatusLv("Max_MP")}");
    }

    public void GetStatusInfo()
    {
        StatusInfo_Text[0].text = string.Format($"물리공격력:{m_PlayerStatus.GetStatus("Physical_ATK")}");
        StatusInfo_Text[1].text = string.Format($"마법공격력:{m_PlayerStatus.GetStatus("Magic_ATK")}");
        StatusInfo_Text[2].text = string.Format($"최대체력:{m_PlayerStatus.GetStatus("Max_HP")}");
        StatusInfo_Text[3].text = string.Format($"최대마나:{m_PlayerStatus.GetStatus("Max_MP")}");
    }

    public void MonsterInfoActive()
    {
        PlayerCharacter Player = m_Player.GetComponent<PlayerCharacter>();
        if (Player.GetAttackMonster() != null)
        {
            m_MonsterInfo.SetActive(true);
        }
        else
        {
            m_MonsterInfo.SetActive(false);
        }
    }

    public void RecoveryAction()
    {
        GameManager.Getinstance().RecoveryStat(GameManager.Getinstance().m_Player);
    }

    public void SummonAction()
    {
        GameManager.Getinstance().SummonMonster(GameManager.Getinstance().m_Player);
    }

    public void Ressurection()
    {
        if (GameManager.Getinstance().m_Player.activeSelf == false && m_PlayerStatus.GetStatus("Cur_HP") > 0)
        {
            GameManager.Getinstance().m_Player.SetActive(true);
            GameManager.Getinstance().m_Player.transform.position = new Vector3(0,3,0);
        }
    }

    public void LevelUp()
    {
        m_PlayerStatus.SetStatus("EXP", m_PlayerStatus.GetStatus("Max_EXP"), "+");
    }

    public void TestButtonAction()
    {
        m_TestButton.anchoredPosition = Vector3.Lerp(m_TestButton.anchoredPosition, new Vector3(-225, -5, 0), Time.deltaTime * 6);
    }
    public void TestButtonReturn()
    {
        m_TestButton.anchoredPosition = Vector3.Lerp(m_TestButton.anchoredPosition, new Vector3(200, -5, 0), Time.deltaTime * 6);
    }

    public void TestButtonClick()
    {
        if(input == true)
        {
            input = false;
        }
        else if(input == false)
        {
            input = true;
        }
    }

    public void ToggleUI(GameObject target)  // 한개만 하는게 아니라 메뉴 바 전체의 UI를 관리하도록
    {
        if(target.activeSelf == false)
        {
            target.SetActive(true);
        }
        else if(target.activeSelf == true)
        {
            target.SetActive(false);
        }
    }

    public void ToggleAllUI(int num )
    {
        for(int i = 0; i < UI_obj.Count; i++)
        {
            if (num == i)
            {
                if(UI_obj[i].activeSelf)
                    UI_obj[i].SetActive(false);
                else
                    UI_obj[i].SetActive(true);
            }
            else
            {
                UI_obj[i].SetActive(false);
            }
        }
    }

    public void ToggleSkillUI(int num)
    {
        for(int i = 0; i < SkillUI.Count; i++)
        {
            if(num == i)
            {
                SkillUI[i].SetActive(true);
            }
            else
            {
                SkillUI[i].SetActive(false);
            }
        }
    }

    public void GetPlayerSkillLv()
    {
        for (int i = 0; i < Skilllv.Count; i++)
        {
            Skilllv[i].text = string.Format($"레벨{m_PlayerSkill.GetMagicLevel(i)}");
        }
    }

}
