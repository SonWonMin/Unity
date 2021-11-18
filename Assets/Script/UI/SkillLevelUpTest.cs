using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLevelUpTest : MonoBehaviour
{
    public GameObject LevelUpTarget;
    SkillSetting PlayerSkill;
    Status TargetStatus;

    
    // 구현으로 일단 이렇게 해두고 이후에 UI에 맞게 수정

    // Start is called before the first frame update
    void Start()
    {
        PlayerSkill = LevelUpTarget.GetComponent<SkillSetting>();
        TargetStatus = LevelUpTarget.GetComponent<Status>();
    }

    public void MultiShotUp()
    {
        if (TargetStatus.GetStatus("SkillPoint") > 0)
        {
            PlayerSkill.AddMagicSkill("MultiRangeAttack");
            TargetStatus.SetStatus("SkillPoint", 1, "-");
        }
    }

    public void RangedUp()
    {
        if (TargetStatus.GetStatus("SkillPoint") > 0 && PlayerSkill.GetMagicLevel(0) >= 5)
        {
            PlayerSkill.AddMagicSkill("RangedAttack");
            TargetStatus.SetStatus("SkillPoint", 1, "-");
        }
    }

    public void IceRangeUp()
    {
        if (TargetStatus.GetStatus("SkillPoint") > 0 && PlayerSkill.GetMagicLevel(1) >= 5)
        {
            PlayerSkill.AddMagicSkill("IceRangeAttack");
            TargetStatus.SetStatus("SkillPoint", 1, "-");
        }
    }
}
