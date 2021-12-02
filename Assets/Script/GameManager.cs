using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject m_Player;
    public GameObject m_SummonMonster;
    static GameManager instance;
    // Start is called before the first frame update

    public static GameManager Getinstance()
    {
        return instance;
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        instance = this;
    }

    public void SummonMonster(GameObject target)
    {
        GameObject CopyMonster = Instantiate(m_SummonMonster, target.transform.position + target.transform.forward * 5, Quaternion.identity);
    }

    public void RecoveryStat(GameObject stat)
    {
        Status status = stat.GetComponent<Status>();
        status.SetStatus("HP", status.GetOriginalStatus("HP"));
        status.SetStatus("MP", status.GetOriginalStatus("MP"));
    }
}
