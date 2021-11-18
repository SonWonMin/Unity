using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSetting : MonoBehaviour
{
    enum MagicSkill
    {
        MultiRangeAttack,
        RangedAttack,
        IceRangeAttack,
        Final
    }

    public List<int> MagicLevel = new List<int>();
    public List<float> MagicCoolTime = new List<float>();

    private void Awake()
    {
        for (int i = 0; i < (int)MagicSkill.Final; i++)
        {
            MagicCoolTime.Add(0);
        }
    }

    private void Update()
    {
        for (int i = 0; i < MagicCoolTime.Count; i++)
        {
            if (MagicCoolTime[i] > 0)
            {
                MagicCoolTime[i] -= Time.deltaTime;
            }

            if(MagicCoolTime[i] < 0)
            {
                MagicCoolTime[i] = 0;
            }
        }
    }

    public int GetMagicLevel(int number)
    {
        return MagicLevel[number];
    }

    public void AddMagicSkill(string skillName)
    {
        switch(skillName)
        {
            case "MultiRangeAttack":
                MagicLevel[0]++;
                break;
            case "RangedAttack":
                MagicLevel[1]++;
                break;
            case "IceRangeAttack":
                MagicLevel[2]++;
                break;
        }
    }

    public float GetCoolTime(string skillName)
    {
        switch(skillName)
        {
            case "MultiRangeAttack":
                return MagicCoolTime[0];
            case "RangedAttack":
                return MagicCoolTime[1];
            case "IceRangeAttack":
                return MagicCoolTime[2];
            default:
                return 0;
        }
    }

    public void SetCoolTime(string skillName, float coolTime)
    {
        switch (skillName)
        {
            case "MultiRangeAttack":
                MagicCoolTime[0] = coolTime;
                break;
            case "RangedAttack":
                MagicCoolTime[1] = coolTime;
                break;
            case "IceRangeAttack":
                MagicCoolTime[2] = coolTime;
                break;
        }
    }
}
