using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemList : MonoBehaviour
{
    [SerializeField]
    List<GameObject> ItemList = new List<GameObject>();
    List<float> DropPercent_List = new List<float>();
    float DropPercentSet = 100;

    // Start is called before the first frame update
    void Start()
    {
        PercentCal();
    }

    public void DropItem()
    {
        float RandPercent = Random.Range(0.0f, 100.0f);
        float Between_First = 0.0f;
        float Between_Sec = DropPercentSet;

        for (int i = 0; i < ItemList.Count; i++)
        {
            if(RandPercent >= Between_First && RandPercent <= Between_Sec)
            {
                GameObject DropItem = Instantiate(ItemList[i], this.gameObject.transform.position, this.gameObject.transform.rotation);
                break;
            }
            else
            {
                Between_First += DropPercentSet + 0.1f;
                Between_Sec += DropPercentSet;
            }
        }

    }

    void PercentCal()
    {
        DropPercentSet = 100f / ItemList.Count;
        for (int i = 0; i < ItemList.Count; i++)
        {
            //Debug.Log($"Caluating : {this.gameObject.name}");
            DropPercent_List.Add(DropPercentSet);
        }
    }

}
